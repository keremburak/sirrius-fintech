using sirrius.Core;
using sirrius.Data.Entity;
using sirrius.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Helper;

namespace sirrius.Service.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client, int> ClientRepository;

        public ClientService(IRepository<Client, int> ClientRepository)
        {
            this.ClientRepository = ClientRepository;
        }

        public async Task<Client> CreateAsync(Client model)
        {
            return await ClientRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var Client = await ClientRepository.FindByIdAsync(id);
            await ClientRepository.DbDeleteAsync(Client);
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await ClientRepository.FindAllAsync();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await ClientRepository.FindByIdAsync(id);
        }

        public async Task<Client> UpdateAsync(int id, Client model)
        {
            var Client = await ClientRepository.FindByIdAsync(id);

            Client.FirstName = model.FirstName;
            Client.LastName = model.LastName;
            Client.PhoneNumber = model.PhoneNumber;
            Client.Email = model.Email;

            return await ClientRepository.UpdateAsync(Client);
        }

        //public async Task<(bool exist, string message)> IsExist(string text, string fieldname = "name")
        public async Task<(bool exist, string message)> IsExist(string fullname)
        {
            string message = string.Empty;

            var Clients = await GetAllAsync();

            return (Clients.Any(q => SirriusHelper.fullTextSearch(q.FullName, fullname.ToLower())), "Client already exist");
        }

        public async Task<Client> GetByUserIdAsync(int id)
        {
            var Clients = await ClientRepository.FindAllAsync();
            return Clients.FirstOrDefault(x => x.UserId == id);
        }
    }
}
