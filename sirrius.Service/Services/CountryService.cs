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
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country, int> countryRepository;

        public CountryService(IRepository<Country, int> countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public async Task<Country> CreateAsync(Country model)
        {
            return await countryRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var country = await countryRepository.FindByIdAsync(id);
            await countryRepository.DbDeleteAsync(country);
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await countryRepository.FindAllAsync();
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await countryRepository.FindByIdAsync(id);
        }

        public async Task<Country> UpdateAsync(int id, Country model)
        {
            var country = await countryRepository.FindByIdAsync(id);

            country.Code = model.Code;
            country.Name = model.Name;

            return await countryRepository.UpdateAsync(country);
        }

        public async Task<(bool exist, string message)> IsExist(string text, string fieldname = "name")
        {
            string message = string.Empty;

            var countries = await GetAllAsync();

            if (fieldname.Equals("name"))
                return (countries.Any(q => SirriusHelper.fullTextSearch(q.Code, text)), "Country Code already exist");
            else
                return (countries.Any(q => SirriusHelper.fullTextSearch(q.Name, text)), "Countrys Name already exist");
        }
    }
}
