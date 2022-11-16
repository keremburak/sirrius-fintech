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
    public class MyClientFTPMatchingService : IMyClientFTPMatchingService
    {
        private readonly IRepository<MyClientFTPMatching, int> myClientFTPMatchingRepository;
        private readonly IRepository<MyClientAccount, int> myClientAccountRepository;
        private readonly IRepository<MyClientAccountingCode, int> myClientAccountingCodeRepository;
        private readonly IRepository<Company, int> companyRepository;
        private readonly IRepository<OperationCode, int> operationCodeRepository;
        private readonly IRepository<CompanyUser, int> companyUserRepository;

        public MyClientFTPMatchingService(IRepository<MyClientFTPMatching, int> myClientFTPMatchingRepository,
                                                IRepository<MyClientAccount, int> myClientAccountRepository,
                                                IRepository<MyClientAccountingCode, int> myClientAccountingCodeRepository,
                                                IRepository<Company, int> companyRepository,
                                                IRepository<OperationCode, int> operationCodeRepository,
                                                IRepository<CompanyUser, int> companyUserRepository)
        {
            this.myClientFTPMatchingRepository = myClientFTPMatchingRepository;
            this.myClientAccountRepository = myClientAccountRepository;
            this.myClientAccountingCodeRepository = myClientAccountingCodeRepository;
            this.companyRepository = companyRepository;
            this.operationCodeRepository = operationCodeRepository;
            this.companyUserRepository = companyUserRepository;
        }
        public async Task<MyClientFTPMatching> CreateAsync(MyClientFTPMatching model)
        {
            return await myClientFTPMatchingRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var ClientFTPMatching = await myClientFTPMatchingRepository.FindByIdAsync(id);
            await myClientFTPMatchingRepository.DbDeleteAsync(ClientFTPMatching);
        }

        //SOR
        public async Task<IEnumerable<MyClientFTPMatching>> GetAllAsync(User user = null)
        {
            var ClientFTPMatchings = await myClientFTPMatchingRepository.FindAllAsync();
            if (user != null)
            {
                var company = await companyRepository.FindAsync(q => q.Id == user.Id);
                ClientFTPMatchings = ClientFTPMatchings.Where(q => (q.MyClientAccount.CompanyId == company.Id) || ((q.MyClientAccountingCode.CompanyId == company.Id))).ToList();
            }
            return ClientFTPMatchings;
        }

        //SOR
        public async Task<MyClientFTPMatching> GetByIdAsync(int id)
        {
            var ClientFTPMatchings = await myClientFTPMatchingRepository.FindByIdAsync(id);
            if (ClientFTPMatchings != null)
            {
                ClientFTPMatchings.OperationCode = await operationCodeRepository.FindByIdAsync(ClientFTPMatchings.OperationCodeId);
                ClientFTPMatchings.MyClientAccount = await myClientAccountRepository.FindByIdAsync(ClientFTPMatchings.MyClientAccountId);
                ClientFTPMatchings.MyClientAccountingCode = await myClientAccountingCodeRepository.FindByIdAsync(ClientFTPMatchings.MyClientAccountId);
            }
            return ClientFTPMatchings;
        }
        //SOR
        public async Task<IEnumerable<MyClientFTPMatching>> GetFtpMatchingsByCompanyId(int companyId)
        {
            return await myClientFTPMatchingRepository.FindAllAsync(q => (q.MyClientAccount.CompanyId == companyId) || ((q.MyClientAccountingCode.CompanyId == companyId)));
        }


        //SOR
        public async Task<(bool exist, string message)> IsExist(string text, string fieldname = "name")
        {
            string message = string.Empty;

            var bankOperationCodes = await GetAllAsync();

            if (fieldname.Equals("code"))
                return (bankOperationCodes.Any(q => SirriusHelper.fullTextSearch(q.MatchingWord, text)), "FTP Matching Word already exist");
            else
                return (bankOperationCodes.Any(q => SirriusHelper.fullTextSearch(q.Name, text)), "FTP Matching Name already exist");
        }

        public async Task<MyClientFTPMatching> UpdateAsync(int id, MyClientFTPMatching model)
        {

            var ClientFTPMatching = await myClientFTPMatchingRepository.FindByIdAsync(id);

            ClientFTPMatching.MatchingWord = model.MatchingWord;
            ClientFTPMatching.MyClientAccountId = model.MyClientAccountId;
            ClientFTPMatching.MyClientAccountId = model.MyClientAccountId;
            ClientFTPMatching.OperationCodeId = model.OperationCodeId;
            ClientFTPMatching.TransactionTypeId = model.TransactionTypeId;
            ClientFTPMatching.OperationTypeId = model.OperationTypeId;


            return await myClientFTPMatchingRepository.UpdateAsync(ClientFTPMatching);
        }
    }
}
