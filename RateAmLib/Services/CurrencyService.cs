
using RateAmData.Entities;
using RateAmData.Repositories;

namespace RateAmLib.Services
{

    public interface ICurrencyService
    {
        Task<IEnumerable<Currency>> GetAll();
        Task<Currency> GetCurrencyByName(string name);
        Task<Currency> GetCurrencyById(int id);
    }

    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrenciesRepository _repository;

        public CurrencyService(ICurrenciesRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Currency>> GetAll()
        {
            var entities = await _repository.GetAll();
            return MapperConfig.Mapper.Map<List<CurrencyEntity>, List<Currency>>(entities.ToList());
        }

        public async Task<Currency> GetCurrencyById(int id)
        {
            var entity = await _repository.GetCurrencyById(id);
            return MapperConfig.Mapper.Map<Currency>(entity);
        }

        public async Task<Currency> GetCurrencyByName(string name)
        {
            var entity = await _repository.GetCurrencyByName(name);
            return MapperConfig.Mapper.Map<Currency>(entity);
        }
    }
}
