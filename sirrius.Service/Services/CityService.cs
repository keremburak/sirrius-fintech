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
    public class CityService : ICityService
    {
        private readonly IRepository<City, int> cityRepository;
        private readonly IRepository<Country, int> countryRepository;

        public CityService(IRepository<City, int> cityRepository,
                                           IRepository<Country, int> countryRepository)
        {
            this.cityRepository = cityRepository;
            this.countryRepository = countryRepository;

        }

        public async Task<City> CreateAsync(City model)
        {
            return await cityRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var city = await cityRepository.FindByIdAsync(id);
            await cityRepository.DbDeleteAsync(city);
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await cityRepository.FindAllAsync();
        }

        public async Task<City> GetByIdAsync(int id)
        {
            var city = await cityRepository.FindByIdAsync(id)
;
            city.Country = await countryRepository.FindByIdAsync(city.CountryId);
            return city;
        }
        public async Task<City> UpdateAsync(int id, City model)
        {
            var city = await cityRepository.FindByIdAsync(id)
;
            //city.Code = model.Code;
            city.Name = model.Name;
            city.CountryId = model.CountryId;
            return await cityRepository.UpdateAsync(city);
        }
        public async Task<(bool exist, string message)> IsExist(string name)
        {
            string message = string.Empty;

            var cities = await GetAllAsync();

            return (cities.Any(q => SirriusHelper.fullTextSearch(q.Name.ToLower(), name.ToLower())), "Name already exist");
        }
    }
}
