using Microsoft.EntityFrameworkCore;
using RateAmData.Entities;


namespace RateAmData.Repositories
{
    public interface IBanksRepository
    {
        Task SaveAsync(BankEntity entity);
        Task SaveAllAsync(BankEntity[] entity);
        
        Task<IEnumerable<BankEntity>> GetAll();
        Task<BankEntity> GetBankByName(string bankName);
        Task<BankEntity> GetBankById(int id);
    }
    public class BanksRepository : IBanksRepository
    {
        private readonly RateAmDataContext _dbContext;
        private readonly DbSet<BankEntity> _dbSet;

        public BanksRepository(RateAmDataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<BankEntity>();
        }
        public async Task SaveAsync(BankEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BankEntity>> GetAll()
        {
            return await _dbContext.Banks.ToListAsync();
            
        }

        public async Task<BankEntity> GetBankByName(string bankName)
        {
            IQueryable<BankEntity> q = _dbContext.Banks.AsQueryable<BankEntity>();
            return await q.Where(b => b.Name == bankName).FirstAsync();
        }

        public async Task<BankEntity> GetBankById(int id)
        {
            IQueryable<BankEntity> q = _dbContext.Banks.AsQueryable<BankEntity>();
            return await q.Where(b => b.BankId == id).FirstAsync();
        }

        public async Task SaveAllAsync(BankEntity[] entity)
        {
            await _dbSet.AddRangeAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
