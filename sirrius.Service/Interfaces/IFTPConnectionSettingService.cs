using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface IFTPConnectionSettingService : IService
    {
        IEnumerable<FTPConnectionSetting> GetAll();
        Task<IEnumerable<FTPConnectionSetting>> GetAllAsync();
        Task<FTPConnectionSetting> GetByIdAsync(int id);
        Task<FTPConnectionSetting> CreateAsync(FTPConnectionSetting model);
        Task<FTPConnectionSetting> UpdateAsync(int id, FTPConnectionSetting model);
        Task DeleteAsync(int id);
        Task<(bool exist, string message)> IsExist(string text, string fieldname = "host");
    }
}

