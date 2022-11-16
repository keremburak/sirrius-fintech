using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface ICountryService : IService
    {
        Task<IEnumerable<Country>> GetAllAsync();
        Task<Country> GetByIdAsync(int id);
        Task<Country> CreateAsync(Country model);
        Task<Country> UpdateAsync(int id, Country model);
        Task DeleteAsync(int id);
        Task<(bool exist, string message)> IsExist(string text, string fieldname = "name");
    }
}

