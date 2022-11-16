using sirrius.Core;
using sirrius.Data.Entity;
using sirrius.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Services
{
    public class BankStatementService : IBankStatementService
    {
        private readonly IRepository<BankStatement, int> bankStatementRepository;

        public BankStatementService(IRepository<BankStatement, int> bankStatementRepository)
        {
            this.bankStatementRepository = bankStatementRepository;
        }

        public async Task<BankStatement> CreateAsync(BankStatement model)
        {
            return await bankStatementRepository.InsertAsync(model);
        }

        public BankStatement Create(BankStatement model)
        {
            return bankStatementRepository.Insert(model);
        }

        public async Task DeleteAsync(int id)
        {
            var bankStatement = await bankStatementRepository.FindByIdAsync(id);
            await bankStatementRepository.DbDeleteAsync(bankStatement);
        }

        public async Task<IEnumerable<BankStatement>> GetAllAsync()
        {
            return await bankStatementRepository.FindAllAsync();
        }

        public async Task<BankStatement> GetByIdAsync(int id)
        {
            return await bankStatementRepository.FindByIdAsync(id);
        }

        public async Task<BankStatement> UpdateAsync(int id, BankStatement model)
        {
            var bankStatement = await bankStatementRepository.FindByIdAsync(id);

            bankStatement.Sender = model.Sender;
            bankStatement.AccountNumber = model.AccountNumber;
            bankStatement.StatementNumber = model.StatementNumber;
            bankStatement.OpeningBalance = model.OpeningBalance;
            bankStatement.ClosingBalance = model.ClosingBalance;
            bankStatement.ClosingAvailableBalance = model.ClosingAvailableBalance;

            return await bankStatementRepository.UpdateAsync(bankStatement);
        }

        public (bool exist, int id) IsExist(BankStatement model)
        {
            var bankStatement = bankStatementRepository.FindAll()
                                                      .FirstOrDefault(q => q.CompanyId == model.CompanyId &&
                                                                q.BankStatementDate.Date == model.BankStatementDate.Date);

            return (bankStatement != null, bankStatement != null ? bankStatement.Id : 0);
        }
    }
}
