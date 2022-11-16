using sirrius.Core;
using sirrius.Data.Entity;
using sirrius.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Helper;

namespace sirrius.Service.Services
{
    public class BankService : IBankService
    {
        private readonly IRepository<Bank, int> bankRepository;
        private readonly IRepository<Country, int> countryRepository;

        public BankService(IRepository<Bank, int> bankRepository, IRepository<Country, int> countryRepository)
        {
            this.bankRepository = bankRepository;
            this.countryRepository = countryRepository;
        }

        public async Task<Bank> CreateAsync(Bank model)
        {
            return await bankRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var bank = await bankRepository.FindByIdAsync(id);
            await bankRepository.DbDeleteAsync(bank);
        }

        public async Task<IEnumerable<Bank>> GetAllAsync()
        {
            return await bankRepository.FindAllAsync();
        }
        public async Task<Bank> GetByCodeAsync(string code)
        {
            var banks = await GetAllAsync();
            var bank = banks.FirstOrDefault(q => q.Code == code);
            bank.Country = await countryRepository.FindByIdAsync(bank.CountryId);

            return bank;
        }

        public async Task<Bank> GetByIdAsync(int id)
        {
            var bank = await bankRepository.FindByIdAsync(id);
            bank.Country = await countryRepository.FindByIdAsync(bank.CountryId);
            return bank;
        }

        public async Task<(bool exist, string message)> IsExist(string text, string fieldname = "name")
        {
            string message = string.Empty;

            var banks = await GetAllAsync();

            if (fieldname.Equals("code"))
                return (banks.Any(q => SirriusHelper.fullTextSearch(q.Code, text)), "Bank Code already exist");
            else
                return (banks.Any(q => SirriusHelper.fullTextSearch(q.Name, text)), "Bank Name already exist");
        }

        public async Task<Bank> UpdateAsync(int id, Bank model)
        {
            var bank = await bankRepository.FindByIdAsync(id);
            bank.Code = model.Code;
            bank.Name = model.Name;
            bank.CountryId = model.CountryId;
            bank.Description = model.Description;

            return await bankRepository.UpdateAsync(bank);
        }
    }
}
