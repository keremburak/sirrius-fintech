using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface IOperationTypeService : IService
    {
        Task<IEnumerable<OperationType>> GetAllAsync();
        Task<OperationType> GetByIdAsync(int id);
        Task<OperationType> CreateAsync(OperationType model);
        Task<OperationType> UpdateAsync(int id, OperationType model);
        Task DeleteAsync(int id);
        Task<(bool exist, string message)> IsExist(string name);

    }
}

