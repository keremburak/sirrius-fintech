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
    public class DashboardService : IDashboardService
    {
        private readonly IRepository<Client, int> clientRepository;
        private readonly IRepository<Company, int> companyRepository;
        private readonly IRepository<CompanyUser, int> companyUserRepository;
        private readonly IRepository<BankStatement, int> bankStatementRepository;
        private readonly IRepository<BankStatementTransaction, int> bankStatementTransactionRepository;

        public DashboardService(IRepository<Client, int> clientRepository,
                                IRepository<Company, int> companyRepository,
                                IRepository<CompanyUser, int> companyUserRepository,
                                IRepository<BankStatement, int> bankStatementRepository,
                                IRepository<BankStatementTransaction, int> bankStatementTransactionRepository)
        {
            this.clientRepository = clientRepository;
            this.companyRepository = companyRepository;
            this.companyUserRepository = companyUserRepository;
            this.bankStatementRepository = bankStatementRepository;
            this.bankStatementTransactionRepository = bankStatementTransactionRepository;
        }

        public async Task<List<BankStatement>> GetBankStatements(User user)
        {
            var companyIds = new int[] { };
            Company company = null;
            List<BankStatement> bankStatements = new List<BankStatement>();

            if (EnumHelper.GetEnumValueFromString<EnumHelper.Roles>(user.Role.Name) == EnumHelper.Roles.Admin)
            {
                var Client = await clientRepository.FindAsync(x => x.UserId == user.Id);

                var companies = await companyRepository.FindAllAsync(x => x.ClientId == Client.Id);
                companyIds = companies.Select(s => s.Id).ToArray();

                bankStatements.AddRange(await bankStatementRepository.FindAllAsync(q => companyIds.Contains(q.CompanyId)));
            }
            else
            {
                var companyUsers = await companyUserRepository.FindAllAsync(q => q.UserId == user.Id);

                if (companyUsers == null || companyUsers.Count == 0)
                    throw new AppException($"CompanyUser could not found");

                company = await companyRepository.FindByIdAsync(companyUsers.FirstOrDefault().CompanyId);

                if (company == null)
                    throw new AppException($"Company could not found");

                bankStatements.AddRange(await bankStatementRepository.FindAllAsync(q => q.CompanyId == company.Id));
            }

            var bankStatementTransactions = await bankStatementTransactionRepository.FindAllAsync();

            bankStatements.ToList().ForEach(q =>
            {
                q.BankStatementTransactions = bankStatementTransactions.Where(x => x.BankStatementId == q.Id).ToList();
            });

            return bankStatements.ToList();
        }

        //public async Task<List<FTPBankStatement>> GetBankStatements(int userId)
        //{
        //    var folderName = string.Empty;

        //    var companyUser = await companyUserRepository.FindAllAsync(q => q.UserId == userId);

        //    if (companyUser == null || companyUser.Count == 0)
        //        throw new AppException($"CompanyUser could not found");

        //    var company = await companyRepository.FindByIdAsync(companyUser.FirstOrDefault().CompanyId);

        //    if (company == null)
        //        throw new AppException($"Company could not found");

        //    var FTPFileDowloadDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Files", "FTP");

        //    if (!Directory.Exists(FTPFileDowloadDirectory))
        //        throw new AppException($"FTP directory : {FTPFileDowloadDirectory} for  MT940 Files could not found");

        //    var parts = company.Name.Split(" ");

        //    if (parts.Length > 2)
        //        folderName = company.Id.ToString() + "_" + parts[0].ConvertTurkishChars().ReplaceCharSet(new char[] { '-', '/', '.', ' ' }, '_') + "_" +
        //                     parts[1].ConvertTurkishChars().ReplaceCharSet(new char[] { '-', '/', '.', ' ' }, '_');
        //    else
        //        folderName = company.Id.ToString() + "_" + parts[0].ConvertTurkishChars().ReplaceCharSet(new char[] { '-', '/', '.', ' ' }, '_');

        //    FTPFileDowloadDirectory = Path.Combine(FTPFileDowloadDirectory, folderName);

        //    if (!Directory.Exists(FTPFileDowloadDirectory))
        //        throw new AppException($"FTP directory : {FTPFileDowloadDirectory} for MT940 Files of company : {company.Name} could not found");

        //    var files = Directory.GetFiles(FTPFileDowloadDirectory, "*.txt", SearchOption.AllDirectories);

        //    var FTPBankStatements = new List<FTPBankStatement>();

        //    foreach (var file in files)
        //        FTPBankStatements.Add(new FTPBankStatement(file));

        //    return FTPBankStatements;
        //}
    }
}
