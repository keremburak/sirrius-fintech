using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface ICurrencyService
    {
        Task<IEnumerable<Currency>> GetAllAsync();
        Task<Currency> GetByIdAsync(int id);
        Task<Currency> CreateAsync(Currency model);
        Task<Currency> UpdateAsync(int id, Currency model);
        Task DeleteAsync(int id);
        Task<(bool exist, string message)> IsExist(string text, string fieldname = "name");
    }
}

