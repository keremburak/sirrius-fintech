using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface ICategoryService : IService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category model);
        Task<Category> UpdateAsync(int id, Category model);
        Task DeleteAsync(int id);

        Task<(bool exist, string message)> IsExist(string name);
    }
}

