using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface ICompanyBankAccountService
    {
        Task<IEnumerable<CompanyBankAccount>> GetAllAsync();
        Task<IEnumerable<CompanyBankAccount>> GetAllByByCompanyIdBankIdAsync(User user, int bankId = 0, int companyId = 0);
        Task<CompanyBankAccount> GetByIdAsync(int id);
        Task<CompanyBankAccount> CreateAsync(CompanyBankAccount model);
        Task<CompanyBankAccount> UpdateAsync(int id, CompanyBankAccount model);
        Task DeleteAsync(int id);
        Task<(bool exist, string message)> IsExist(string text, string fieldname = "name");
        Task<CompanyBankAccount> GetByAccountNumberAsync(string accountNumber);
    }
}

