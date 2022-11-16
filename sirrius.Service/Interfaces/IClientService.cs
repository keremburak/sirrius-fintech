using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface IClientService : IService
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(int id);
        Task<Client> CreateAsync(Client model);
        Task<Client> UpdateAsync(int id, Client model);
        Task DeleteAsync(int id);

        Task<Client> GetByUserIdAsync(int id);

        Task<(bool exist, string message)> IsExist(string fullname);
    }
}

