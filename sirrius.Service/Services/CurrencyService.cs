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
    public class CurrencyService : ICurrencyService
    {
        private readonly IRepository<Currency, int> currencyRepository;

        public CurrencyService(IRepository<Currency, int> currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }

        public async Task<Currency> CreateAsync(Currency model)
        {
            return await currencyRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var currency = await currencyRepository.FindByIdAsync(id);
            await currencyRepository.DbDeleteAsync(currency);
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            return await currencyRepository.FindAllAsync();
        }

        public async Task<Currency> GetByIdAsync(int id)
        {
            return await currencyRepository.FindByIdAsync(id);
        }

        public async Task<(bool exist, string message)> IsExist(string text, string fieldname = "name")
        {
            string message = string.Empty;

            var currencies = await GetAllAsync();

            //if (fieldname.Equals("code"))
            //    return (currencies.Any(q => SirriusHelper.fullTextSearch(q.Code, text)), "Currenciy  already exist");
            //else
            return (currencies.Any(q => SirriusHelper.fullTextSearch(q.Name, text)), "Currency Name already exist");
        }

        public async Task<Currency> UpdateAsync(int id, Currency model)
        {
            var currency = await currencyRepository.FindByIdAsync(id);

            // currency.Code = model.Code;
            currency.Name = model.Name;
            currency.Description = model.Description;

            return await currencyRepository.UpdateAsync(currency);
        }
    }
}
