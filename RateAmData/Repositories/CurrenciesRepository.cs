using Microsoft.EntityFrameworkCore;
using RateAmData.Entities;


namespace RateAmData.Repositories
{
    public interface ICurrenciesRepository
    {
        Task SaveAll(CurrencyEntity[] entity);
        Task<IEnumerable<CurrencyEntity>> GetAll();
        Task<CurrencyEntity> GetCurrencyByName(string name);
        Task<CurrencyEntity> GetCurrencyById(int id);
    }
    public class CurrenciesRepository : ICurrenciesRepository
    {
        private readonly RateAmDataContext _dbContext;
        private readonly DbSet<CurrencyEntity> _dbSet;

        public CurrenciesRepository(RateAmDataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<CurrencyEntity>();
        }

        public async Task<IEnumerable<CurrencyEntity>> GetAll()
        {
            var cur = await _dbContext.Currencies.ToListAsync();
            return cur;
        }

        public async Task<CurrencyEntity> GetCurrencyById(int id)
        {
            IQueryable<CurrencyEntity> q = _dbContext.Currencies.AsQueryable<CurrencyEntity>();
            return await q.Where(b => b.CurrencyId == id).FirstAsync();
        }

        public async Task<CurrencyEntity> GetCurrencyByName(string name)
        {
            IQueryable<CurrencyEntity> q = _dbContext.Currencies.AsQueryable<CurrencyEntity>();
            return await q.Where(b => b.Name == name).FirstAsync();
        }

        public async Task SaveAll(CurrencyEntity[] entity)
        {
            await _dbSet.AddRangeAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
