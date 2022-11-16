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
    public class CompanyBankAccountService : ICompanyBankAccountService
    {
        private readonly IRepository<Client, int> clientRepository;
        private readonly IRepository<Company, int> companyRepository;
        private readonly IRepository<CompanyUser, int> companyUserRepository;
        private readonly IRepository<Bank, int> bankRepository;
        private readonly IRepository<Currency, int> currencyRepository;
        private readonly IRepository<CompanyBankAccount, int> companyBankAccountRepository;

        public CompanyBankAccountService(IRepository<Client, int> clientRepository,
                                         IRepository<Company, int> companyRepository,
                                         IRepository<CompanyUser, int> companyUserRepository,
                                         IRepository<Bank, int> bankRepository,
                                         IRepository<Currency, int> currencyRepository,
                                         IRepository<CompanyBankAccount, int> companyBankAccountRepository)
        {
            this.clientRepository = clientRepository;
            this.companyRepository = companyRepository;
            this.companyUserRepository = companyUserRepository;
            this.bankRepository = bankRepository;
            this.currencyRepository = currencyRepository;
            this.companyBankAccountRepository = companyBankAccountRepository;
        }

        public async Task<CompanyBankAccount> CreateAsync(CompanyBankAccount model)
        {
            return await companyBankAccountRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var account = await companyBankAccountRepository.FindByIdAsync(id);
            await companyBankAccountRepository.DbDeleteAsync(account);
        }

        public async Task<IEnumerable<CompanyBankAccount>> GetAllAsync()
        {
            return await companyBankAccountRepository.FindAllAsync();
        }

        public async Task<IEnumerable<CompanyBankAccount>> GetAllByByCompanyIdBankIdAsync(User user, int bankId = 0, int companyId = 0)
        {
            var companyIds = new int[] { };
            Company company = null;
            List<CompanyBankAccount> companyBankAccounts = new List<CompanyBankAccount>();

            if (EnumHelper.GetEnumValueFromString<EnumHelper.Roles>(user.Role.Name) == EnumHelper.Roles.Admin)
            {

                if (companyId > 0)
                {
                    companyBankAccounts.AddRange(await companyBankAccountRepository.FindAllAsync(x => x.CompanyId == companyId));
                }
                else
                {
                    var Client = await clientRepository.FindAsync(x => x.UserId == user.Id);

                    var companies = await companyRepository.FindAllAsync(x => x.ClientId == Client.Id);
                    companyIds = companies.Select(s => s.Id).ToArray();

                    if (bankId > 0)
                        companyBankAccounts.AddRange(await companyBankAccountRepository.FindAllAsync(q => companyIds.Contains(q.CompanyId) && q.BankId == bankId));
                    else
                        companyBankAccounts.AddRange(await companyBankAccountRepository.FindAllAsync(q => companyIds.Contains(q.CompanyId)));
                }
            }
            else
            {
                var companyUsers = await companyUserRepository.FindAsync(q => q.UserId == user.Id);

                if (companyUsers == null)
                    throw new AppException($"CompanyUser could not found");

                company = await companyRepository.FindByIdAsync(companyUsers.CompanyId);

                if (company == null)
                    throw new AppException($"Company could not found");

                if (bankId > 0)
                    companyBankAccounts.AddRange(await companyBankAccountRepository.FindAllAsync(q => q.CompanyId == company.Id && q.BankId == bankId));
                else
                    companyBankAccounts.AddRange(await companyBankAccountRepository.FindAllAsync(q => q.CompanyId == company.Id));
            }

            var banks = await bankRepository.FindAllAsync();
            var currencies = await currencyRepository.FindAllAsync();

            companyBankAccounts.ToList().ForEach(x =>
            {
                x.Bank = banks.FirstOrDefault(q => q.Id == x.BankId);
                x.Currency = currencies.FirstOrDefault(q => q.Id == x.CurrencyId);
            });

            return companyBankAccounts;
        }

        public async Task<CompanyBankAccount> GetByAccountNumberAsync(string accountNumber)
        {
            return await companyBankAccountRepository.FindAsync(q => q.AccountNumber.Trim().Equals(accountNumber.Trim()));
        }

        public async Task<CompanyBankAccount> GetByIdAsync(int id)
        {
            var companyBankAccount = await companyBankAccountRepository.FindByIdAsync(id);
            companyBankAccount.Company = await companyRepository.FindByIdAsync(companyBankAccount.CompanyId);
            companyBankAccount.Bank = await bankRepository.FindByIdAsync(companyBankAccount.BankId);
            companyBankAccount.Currency = await currencyRepository.FindByIdAsync(companyBankAccount.CurrencyId);
            return companyBankAccount;
        }

        public async Task<(bool exist, string message)> IsExist(string text, string fieldname = "name")
        {
            string message = string.Empty;

            var companyBankAccounts = await GetAllAsync();

            if (fieldname.Equals("iban"))
                return (companyBankAccounts.Any(q => SirriusHelper.fullTextSearch(q.IBAN, text)), "IBAN already exist");
            else
                return (companyBankAccounts.Any(q => SirriusHelper.fullTextSearch(q.Name, text)), "Account Name already exist");
        }

        public async Task<CompanyBankAccount> UpdateAsync(int id, CompanyBankAccount model)
        {
            var companyBankAccount = await companyBankAccountRepository.FindByIdAsync(id);

            companyBankAccount.CompanyId = model.CompanyId;
            companyBankAccount.BankId = model.BankId;
            companyBankAccount.CurrencyId = model.CurrencyId;
            companyBankAccount.BranchCode = model.BranchCode;
            companyBankAccount.AccountNumber = model.AccountNumber;
            companyBankAccount.IBAN = model.IBAN;

            return await companyBankAccountRepository.UpdateAsync(companyBankAccount);
        }
    }
}
