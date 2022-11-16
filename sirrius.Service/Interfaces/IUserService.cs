using sirrius.Data.Entity;
using sirrius.Model.Entity.Token;
using sirrius.Model.Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface IUserService : IService
    {
        Task<TokenResponse> AuthenticateAsync(AuthenticateRequest model);
        Task<string> GetNewTokenAsync(string refreshToken);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> CreateAsync(User model);
        Task<User> UpdateAsync(int id, User model);
        Task DeleteAsync(int id);
        Task<(bool exist, string message)> IsExist(string username);
        Task<IEnumerable<Role>> GetAllRoleAsync();

        Task<IEnumerable<CompanyUser>> GetAllCompanyUserAsync(IEnumerable<int> companyIds);
    }
}

