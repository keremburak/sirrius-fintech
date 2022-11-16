using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface ICompanyService : IService
    {
        Task<IEnumerable<Company>> GetAllAsync(User user = null);
        Task<Company> GetByIdAsync(int id);
        Task<Company> CreateAsync(Company model);
        Task<Company> UpdateAsync(int id, Company model);
        Task DeleteAsync(int id);
        Task<(bool exist, string message)> IsExist(string text, string fieldname = "name");

        Task<IEnumerable<CompanyUser>> GetCompanyUser(int companyId = 0, int userId = 0);

        Task<IEnumerable<Company>> GetClientCompaniesByUserId(int userId);
        Task<IEnumerable<Company>> GetClientCompaniesByClientId(int ClientId);
    }
}

