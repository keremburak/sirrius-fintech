using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface IMyClientAccountService
    {
        Task<IEnumerable<MyClientAccount>> GetAllAsync(User user = null);
        Task<MyClientAccount> GetByIdAsync(int id);
        Task<MyClientAccount> CreateAsync(MyClientAccount model);
        Task<MyClientAccount> UpdateAsync(int id, MyClientAccount model);
        Task DeleteAsync(int id);
        Task<(bool exist, string message)> IsExist(string text, string fieldname = "name");
        Task<IEnumerable<MyClientAccount>> GetClientAccountByCompanyId(int companyId);
    }
}

