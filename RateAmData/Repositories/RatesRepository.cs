using Microsoft.EntityFrameworkCore;
using RateAmData.Entities;

namespace RateAmData.Repositories;

public interface IRatesRepository
{
    Task Save(RateEntity entity);
    Task SaveAll(RateEntity[] entities);
    Task DeleteAll();
    Task<IEnumerable<RateEntity>> GetAll();
    Task<IEnumerable<RateEntity>> GetByBankId(int bank_id);
    Task<IEnumerable<RateEntity>> GetByCurrencyId(int currency_id);
    Task<IEnumerable<RateEntity>> GetLatestRates();
    event EventHandler<RedisArgs> DataSaved;
}
public class RatesRepository : IRatesRepository
{
    private readonly RateAmDataContext _dbContext;
    private readonly DbSet<RateEntity> _dbSet;
    private readonly DbSet<LastDateEntity> _dbSetDates;

    public event EventHandler<RedisArgs> DataSaved;
    public RatesRepository(RateAmDataContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<RateEntity>();
        _dbSetDates = _dbContext.Set<LastDateEntity>();
    }

    public async Task Save(RateEntity entity)
    {
        await _dbSet.AddAsync(entity);
        //var existingEntity = _dbContext.Set<RateEntity>().Local.FirstOrDefault(e => e.Id == entity.Id);

        //if (existingEntity != null)
        //{
        //    _dbContext.Entry(existingEntity).State = EntityState.Detached;
        //}
        await _dbContext.SaveChangesAsync();
    }
    public async Task SaveAll(RateEntity[] entities)
    {


        var query = from r in _dbContext.Rates
                    join maxPublishDates in
                    (
                        from r1 in _dbContext.Rates
                        group r1 by new { r1.BankId, r1.CurrencyId } into g
                        select new
                        {
                            g.Key.BankId,
                            g.Key.CurrencyId,
                            MaxPublishDate = g.Max(r1 => r1.PublishDate)
                        }
                    ) on new { r.BankId, r.CurrencyId, r.PublishDate } equals
                        new { maxPublishDates.BankId, maxPublishDates.CurrencyId, PublishDate = maxPublishDates.MaxPublishDate }
                    select new
                    {
                        r.BankId,
                        r.CurrencyId,
                        r.PublishDate,
                        r.SellRate,
                        r.BuyRate
                    };
        var latestRates = await query.ToListAsync();

        var latestGroupedByDate = latestRates.Distinct();

        var groupedByBank = entities.GroupBy(r => r.BankId);

        var isChanged = false;

        foreach (var group in groupedByBank)
        {
            int bankId = group.Key;
            var publishDate = group.First().PublishDate;

            if (!latestGroupedByDate.Any(r => r.BankId == bankId && r.PublishDate == publishDate))
            {
                await _dbSet.AddRangeAsync(group);
                isChanged = true;
            }
        }

        // await _dbSet.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
        SaveInDbAndRedis(isChanged);
    }

    public async Task DeleteAll()
    {
        var rates = await GetAll();
        _dbSet.RemoveRange(rates);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<RateEntity>> GetAll() => await _dbContext.Rates.ToListAsync();


    public async Task<IEnumerable<RateEntity>> GetByBankId(int bank_id)
    {
        IQueryable<RateEntity> q = _dbContext.Rates.AsQueryable<RateEntity>();
        return await q.Where(r => r.BankId == bank_id).ToListAsync();
    }

    public async Task<IEnumerable<RateEntity>> GetByCurrencyId(int currency_id)
    {
        IQueryable<RateEntity> q = _dbContext.Rates.AsQueryable<RateEntity>();
        return await q.Where(r => r.CurrencyId == currency_id).ToListAsync();
    }


    public async Task<IEnumerable<RateEntity>> GetLatestRates()
    {
        IQueryable<RateEntity> qlatestRates = _dbContext.Rates.AsQueryable<RateEntity>();

        var rates = await qlatestRates.ToListAsync();
        rates = rates.GroupBy(rate => new { rate.BankId, rate.CurrencyId })
        .Select(group => group.OrderBy(r => r.PublishDate).Last())
        .OrderBy(result => result.BankId)
        .ThenBy(result => result.CurrencyId).ToList();

        return rates;

    }

    private async void SaveInDbAndRedis(bool isChanged)
    {
        if (isChanged)
        {
            var lastDate = DateTime.Now.AddHours(+4).ToUniversalTime();
            var latestR = await GetLatestRates();

            await _dbSetDates.AddAsync(new LastDateEntity { LastDate = lastDate });
            await _dbContext.SaveChangesAsync();

            DataSaved(this, new RedisArgs(lastDate, latestR));
        }

    }
}

public class RedisArgs : EventArgs
{
    public DateTime Date { get; set; }
    public IEnumerable<RateEntity> Rates { get; set; }

    public RedisArgs(DateTime date, IEnumerable<RateEntity> rates)
    {
        Date = date;
        Rates = rates;
    }
}