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
    public class MyClientAccountingCodeService : IMyClientAccountingCodeService
    {
        private readonly IRepository<MyClientAccountingCode, int> myClientAccountingCodeRepository;
        private readonly IRepository<Company, int> companyRepository;
        private readonly IRepository<MyClientFTPMatching, int> ftpMatchingRepository;
        private readonly IRepository<CompanyUser, int> companyUserRepository;

        public MyClientAccountingCodeService(IRepository<MyClientAccountingCode, int> myClientAccountingCodeRepository,
                                               IRepository<Company, int> companyRepository,
                                               IRepository<MyClientFTPMatching, int> ftpMatchingRepository,
                                               IRepository<CompanyUser, int> companyUserRepository)
        {
            this.myClientAccountingCodeRepository = myClientAccountingCodeRepository;
            this.companyRepository = companyRepository;
            this.ftpMatchingRepository = ftpMatchingRepository;
            this.companyUserRepository = companyUserRepository;
        }

        public async Task<MyClientAccountingCode> CreateAsync(MyClientAccountingCode model)
        {
            return await myClientAccountingCodeRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var ClientAccountingCode = await myClientAccountingCodeRepository.FindByIdAsync(id);
            await myClientAccountingCodeRepository.DbDeleteAsync(ClientAccountingCode);
        }

        public async Task<IEnumerable<MyClientAccountingCode>> GetAllAsync(User user = null)
        {
            return await myClientAccountingCodeRepository.FindAllAsync();
        }

        public async Task<MyClientAccountingCode> GetByIdAsync(int id)
        {
            var ClientAccountingCodes = await myClientAccountingCodeRepository.FindByIdAsync(id);
            if (ClientAccountingCodes != null)
            {
                ClientAccountingCodes.Company = await companyRepository.FindByIdAsync(ClientAccountingCodes.CompanyId);
                ClientAccountingCodes.MyClientFTPMatching = await ftpMatchingRepository.FindByIdAsync(ClientAccountingCodes.MyClientFTPMatchingId);
            }
            return ClientAccountingCodes;
        }

        public async Task<IEnumerable<MyClientAccountingCode>> GetClientAccountingCodeByCompanyId(int companyId)
        {
            return await myClientAccountingCodeRepository.FindAllAsync(q => q.CompanyId == companyId);
        }

        public async Task<(bool exist, string message)> IsExist(string text, string fieldname = "name")
        {
            string message = string.Empty;
            var ClientAccountingCodes = await GetAllAsync();

            if (fieldname.Equals("Code"))
                return (ClientAccountingCodes.Any(q => SirriusHelper.fullTextSearch(q.Code, text)), "Client Accounting Code already exist");
            else
                return (ClientAccountingCodes.Any(q => SirriusHelper.fullTextSearch(q.Name, text)), "Client Accounting Code Name already exist");
        }

        public async Task<MyClientAccountingCode> UpdateAsync(int id, MyClientAccountingCode model)
        {
            var ClientAccountingCode = await myClientAccountingCodeRepository.FindByIdAsync(id);

            ClientAccountingCode.CompanyId = model.CompanyId;
            ClientAccountingCode.Code = model.Code;
            ClientAccountingCode.Description = model.Description;
            ClientAccountingCode.MyClientFTPMatchingId = model.MyClientFTPMatchingId;

            return await myClientAccountingCodeRepository.UpdateAsync(ClientAccountingCode);
        }
    }
}
