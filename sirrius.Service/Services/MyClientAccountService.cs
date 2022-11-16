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
    public class MyClientAccountService : IMyClientAccountService
    {
        private readonly IRepository<MyClientAccount, int> myClientAccountRepository;
        private readonly IRepository<Company, int> companyRepository;
        private readonly IRepository<MyClientFTPMatching, int> ftpMatchingRepository;
        private readonly IRepository<CompanyUser, int> companyUserRepository;

        public MyClientAccountService(IRepository<MyClientAccount, int> myClientAccountRepository,
                                               IRepository<Company, int> companyRepository,
                                               IRepository<MyClientFTPMatching, int> ftpMatchingRepository,
                                               IRepository<CompanyUser, int> companyUserRepository)
        {
            this.myClientAccountRepository = myClientAccountRepository;
            this.companyRepository = companyRepository;
            this.ftpMatchingRepository = ftpMatchingRepository;
            this.companyUserRepository = companyUserRepository;
        }
        public async Task<MyClientAccount> CreateAsync(MyClientAccount model)
        {
            return await myClientAccountRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var ClientAccount = await myClientAccountRepository.FindByIdAsync(id);
            await myClientAccountRepository.DbDeleteAsync(ClientAccount);
        }

        public async Task<IEnumerable<MyClientAccount>> GetAllAsync(User user = null)
        {
            return await myClientAccountRepository.FindAllAsync();
        }

        public async Task<MyClientAccount> GetByIdAsync(int id)
        {
            var ClientAccount = await myClientAccountRepository.FindByIdAsync(id);
            if (ClientAccount != null)
            {
                ClientAccount.Company = await companyRepository.FindByIdAsync(ClientAccount.CompanyId);
                ClientAccount.MyClientFTPMatching = await ftpMatchingRepository.FindByIdAsync(ClientAccount.MyClientFTPMatchingId);
            }
            return ClientAccount;
        }

        public async Task<IEnumerable<MyClientAccount>> GetClientAccountByCompanyId(int companyId)
        {
            return await myClientAccountRepository.FindAllAsync(q => q.CompanyId == companyId);
        }

        public async Task<(bool exist, string message)> IsExist(string text, string fieldname = "name")
        {
            string message = string.Empty;
            var ClientAccount = await GetAllAsync();

            if (fieldname.Equals("Code"))
                return (ClientAccount.Any(q => SirriusHelper.fullTextSearch(q.Code, text)), "Client Account already exist");
            else
                return (ClientAccount.Any(q => SirriusHelper.fullTextSearch(q.Name, text)), "Client Account Name already exist");
        }

        public async Task<MyClientAccount> UpdateAsync(int id, MyClientAccount model)
        {
            var ClientAccount = await myClientAccountRepository.FindByIdAsync(id);

            ClientAccount.CompanyId = model.CompanyId;
            ClientAccount.Code = model.Code;
            ClientAccount.Description = model.Description;
            ClientAccount.MyClientFTPMatchingId = model.MyClientFTPMatchingId;

            return await myClientAccountRepository.UpdateAsync(ClientAccount);
        }
    }
}
