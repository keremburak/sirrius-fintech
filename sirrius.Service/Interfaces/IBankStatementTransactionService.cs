using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface IBankStatementTransactionService
    {
        Task<IEnumerable<BankStatementTransaction>> GetAllAsync();
        Task<BankStatementTransaction> GetByIdAsync(int id);
        Task<BankStatementTransaction> CreateAsync(BankStatementTransaction model);
        Task<BankStatementTransaction> UpdateAsync(int id, BankStatementTransaction model);
        Task DeleteAsync(int id);

        BankStatementTransaction Create(BankStatementTransaction model);
    }
}

