using Newtonsoft.Json;
using RateAmData.Entities;
using RateAmData.Repositories;
using RateAmLib.Utils;

namespace RateAmLib.Services
{
    public interface IRateService
    {
        Task<IEnumerable<Rate>> GetAllRatesAsync();
        Task<IEnumerable<Rate>> GetLatestRates();
        IEnumerable<Rate> GetAllRates();
        Task SaveRateAsync(Rate rate);
        Task SaveAll(Rate[] rates);
        Task DeleteAllAsync();
        Task<IEnumerable<Rate>> GetRatesByBankIdAsync(int id);
        Task<IEnumerable<Rate>> GetRatesByCurrencyIdAsync(int id);
        Task<List<Rate>> GetRateByDateAsync(DateTime date);
    }

    public class RateService : IRateService
    {
        private IRatesRepository _repository { get; set; }

        private IRedisCache _redisCache { get; set; }


        public RateService(IRatesRepository RateRepo)
        {
            _repository = RateRepo;
            _repository.DataSaved += UpdateCache;
            _redisCache = RedisCache.GetRedisCache();
        }

        public async Task SaveRateAsync(Rate rate)
        {
            var entity = MapperConfig.Mapper.Map<Rate, RateEntity>(rate);
            await _repository.Save(entity);
        }

        public async Task DeleteAllAsync()
        {
            await _repository.DeleteAll();
        }

        public async Task<IEnumerable<Rate>> GetAllRatesAsync()
        {
            var rates = await _repository.GetAll();
            return rates.Select(x => MapperConfig.Mapper.Map<RateEntity, Rate>(x));
        }

        public IEnumerable<Rate> GetAllRates()
        {
            var rates = _redisCache.Get("rates");

            if (!string.IsNullOrEmpty(rates))
            {
                var ratesArray = JsonConvert.DeserializeObject<Rate[]>(rates);

                return ratesArray;
            }

            var ratesFromDb = GetLatestRates().Result;

            string json = System.Text.Json.JsonSerializer.Serialize(ratesFromDb);
            _redisCache.Set("rates", json);

            return ratesFromDb;
        }
        public async Task<IEnumerable<Rate>> GetRatesByBankIdAsync(int id)
        {
            var entities = await _repository.GetByBankId(id);
            return entities.Select(x => MapperConfig.Mapper.Map<RateEntity, Rate>(x));
        }
        public async Task<IEnumerable<Rate>> GetRatesByCurrencyIdAsync(int id)
        {
            var entities = await _repository.GetByCurrencyId(id);
            return entities.Select(x => MapperConfig.Mapper.Map<RateEntity, Rate>(x));
        }
        public Task<List<Rate>> GetRateByDateAsync(DateTime date)
        {
            return null;
        }

        public async Task SaveAll(Rate[] rates)
        {
            var entities = rates.Select(x => MapperConfig.Mapper.Map<Rate, RateEntity>(x));
            await _repository.SaveAll(entities.ToArray());
        }

        public async Task<IEnumerable<Rate>> GetLatestRates()
        {
            var rates = await _repository.GetLatestRates();
            return rates.Select(x => MapperConfig.Mapper.Map<RateEntity, Rate>(x));
        }

        private void UpdateCache(object sender, RedisArgs e)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(e.Rates);
            _redisCache.Set("rates", json);
            _redisCache.Set("lastUpdated", e.Date.ToString());
        }
    }
}
