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
    public class BankStatementTransactionService : IBankStatementTransactionService
    {
        private readonly IRepository<BankStatementTransaction, int> bankStatementTransactionRepository;

        public BankStatementTransactionService(IRepository<BankStatementTransaction, int> bankStatementTransactionRepository)
        {
            this.bankStatementTransactionRepository = bankStatementTransactionRepository;
        }

        public async Task<BankStatementTransaction> CreateAsync(BankStatementTransaction model)
        {
            return await bankStatementTransactionRepository.InsertAsync(model);
        }

        public BankStatementTransaction Create(BankStatementTransaction model)
        {
            return bankStatementTransactionRepository.Insert(model);
        }

        public async Task DeleteAsync(int id)
        {
            var bankStatementTransaction = await bankStatementTransactionRepository.FindByIdAsync(id);
            await bankStatementTransactionRepository.DbDeleteAsync(bankStatementTransaction);
        }

        public async Task<IEnumerable<BankStatementTransaction>> GetAllAsync()
        {
            return await bankStatementTransactionRepository.FindAllAsync();
        }

        public async Task<BankStatementTransaction> GetByIdAsync(int id)
        {
            return await bankStatementTransactionRepository.FindByIdAsync(id);
        }

        public async Task<BankStatementTransaction> UpdateAsync(int id, BankStatementTransaction model)
        {
            var bankStatementTransaction = await bankStatementTransactionRepository.FindByIdAsync(id);

            bankStatementTransaction.TransactionDate = model.TransactionDate;
            bankStatementTransaction.IsCredit = model.IsCredit;
            bankStatementTransaction.CurrencyCode = model.CurrencyCode;
            bankStatementTransaction.Amount = model.Amount;
            bankStatementTransaction.OperationType = model.OperationType;
            bankStatementTransaction.OperationCode = model.OperationCode;
            bankStatementTransaction.IsTransferred = model.IsTransferred;
            bankStatementTransaction.ClientReference = model.ClientReference;
            bankStatementTransaction.BankReference = model.BankReference;
            bankStatementTransaction.Description = model.Description;

            return await bankStatementTransactionRepository.UpdateAsync(bankStatementTransaction);
        }

        public Task<IEnumerable<BankStatementTransaction>> GetAllByCompanyIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
