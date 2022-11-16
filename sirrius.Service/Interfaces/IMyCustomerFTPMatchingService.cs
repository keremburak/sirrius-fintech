using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface IMyClientFTPMatchingService : IService
    {
        Task<IEnumerable<MyClientFTPMatching>> GetAllAsync(User user = null);
        Task<MyClientFTPMatching> GetByIdAsync(int id);
        Task<MyClientFTPMatching> CreateAsync(MyClientFTPMatching model);
        Task<MyClientFTPMatching> UpdateAsync(int id, MyClientFTPMatching model);
        Task DeleteAsync(int id);
        Task<(bool exist, string message)> IsExist(string text, string fieldname = "name");
        Task<IEnumerable<MyClientFTPMatching>> GetFtpMatchingsByCompanyId(int companyId);
    }
}

