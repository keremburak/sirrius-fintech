using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface IOperationCodeService : IService
    {
        Task<IEnumerable<OperationCode>> GetAllAsync();
        Task<OperationCode> GetByIdAsync(int id);
        Task<OperationCode> CreateAsync(OperationCode model);
        Task<OperationCode> UpdateAsync(int id, OperationCode model);
        Task DeleteAsync(int id);
        Task<(bool exist, string message)> IsExist(string text, string fieldname = "name");
    }
}

