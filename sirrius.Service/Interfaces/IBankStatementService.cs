using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface IBankStatementService
    {
        Task<IEnumerable<BankStatement>> GetAllAsync();
        Task<BankStatement> GetByIdAsync(int id);
        Task<BankStatement> CreateAsync(BankStatement model);
        Task<BankStatement> UpdateAsync(int id, BankStatement model);
        Task DeleteAsync(int id);

        BankStatement Create(BankStatement model);

        (bool exist, int id) IsExist(BankStatement model);
    }
}

