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
    public class OperationCodeService : IOperationCodeService
    {
        private readonly IRepository<OperationCode, int> OperationCodeRepository;

        public OperationCodeService(IRepository<OperationCode, int> bankOpertaionCodeRepository)
        {
            this.OperationCodeRepository = bankOpertaionCodeRepository;
        }

        public async Task<OperationCode> CreateAsync(OperationCode model)
        {
            return await OperationCodeRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var OperationCode = await OperationCodeRepository.FindByIdAsync(id);
            await OperationCodeRepository.DbDeleteAsync(OperationCode);
        }

        public async Task<IEnumerable<OperationCode>> GetAllAsync()
        {
            return await OperationCodeRepository.FindAllAsync();
        }

        public async Task<OperationCode> GetByIdAsync(int id)
        {
            return await OperationCodeRepository.FindByIdAsync(id);
        }

        public async Task<(bool exist, string message)> IsExist(string text, string fieldname = "name")
        {
            string message = string.Empty;

            var OperationCodes = await GetAllAsync();

            if (fieldname.Equals("code"))
                return (OperationCodes.Any(q => SirriusHelper.fullTextSearch(q.Code, text)), "Operation Code already exist");
            else
                return (OperationCodes.Any(q => SirriusHelper.fullTextSearch(q.Name, text)), "Operation Name already exist");
        }

        public async Task<OperationCode> UpdateAsync(int id, OperationCode model)
        {
            var bankOpertaionCode = await OperationCodeRepository.FindByIdAsync(id);

            bankOpertaionCode.Code = model.Code;
            bankOpertaionCode.Name = model.Name;
            bankOpertaionCode.Description = model.Description;

            return await OperationCodeRepository.UpdateAsync(bankOpertaionCode);
        }
    }
}
