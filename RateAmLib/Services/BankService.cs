using AutoMapper;
using OpenQA.Selenium.DevTools.V120.Runtime;
using RateAmData.Entities;
using RateAmData.Repositories;


namespace RateAmLib.Services
{
    public interface IBankService
    {
        Task<Bank> GetBankByName(string bankName);
        Task<Bank> GetBankById(int id);
        Task<IEnumerable<Bank>> GetAll();
        Task SaveAll(Bank[] banks);
    }

    public class BankService : IBankService
    {
        private readonly IBanksRepository _repository;
        

        public BankService(IBanksRepository bankRepo)
        {
            _repository = bankRepo;
        }
        public async Task<IEnumerable<Bank>> GetAll()
        {
            var entities = await _repository.GetAll();
            return MapperConfig.Mapper.Map<List<BankEntity>, List<Bank>>(entities.ToList());
        }

        public async Task<Bank> GetBankById(int id)
        {
            var entity = await _repository.GetBankById(id);
            return MapperConfig.Mapper.Map<Bank>(entity);
        }

        public async Task<Bank> GetBankByName(string bankName)
        {
            var entity = await _repository.GetBankByName(bankName);
            return MapperConfig.Mapper.Map<Bank>(entity);
        }

        public async Task SaveAll(Bank[] banks)
        {
            var entities = banks.Select(x => MapperConfig.Mapper.Map<Bank, BankEntity>(x));
            await _repository.SaveAllAsync(entities.ToArray());
        }
    }
}
