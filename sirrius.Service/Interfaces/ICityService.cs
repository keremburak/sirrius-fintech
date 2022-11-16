using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface ICityService : IService
    {
        Task<IEnumerable<City>> GetAllAsync();
        Task<City> GetByIdAsync(int id);
        Task<City> CreateAsync(City model);
        Task<City> UpdateAsync(int id, City model);
        Task DeleteAsync(int id);
        Task<(bool exist, string message)> IsExist(string name);
    }
}

