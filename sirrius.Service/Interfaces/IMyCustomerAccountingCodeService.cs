using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface IMyClientAccountingCodeService : IService
    {
        Task<IEnumerable<MyClientAccountingCode>> GetAllAsync(User user = null);
        Task<MyClientAccountingCode> GetByIdAsync(int id);
        Task<MyClientAccountingCode> CreateAsync(MyClientAccountingCode model);
        Task<MyClientAccountingCode> UpdateAsync(int id, MyClientAccountingCode model);
        Task DeleteAsync(int id);
        Task<(bool exist, string message)> IsExist(string text, string fieldname = "name");
        Task<IEnumerable<MyClientAccountingCode>> GetClientAccountingCodeByCompanyId(int companyId);
    }
}

