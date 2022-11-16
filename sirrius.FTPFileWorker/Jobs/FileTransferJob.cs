using Microsoft.Extensions.Configuration;
using Quartz;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using sirrius.Model.DataModel;
using sirrius.Service.Interfaces;
using Utilities.Helper;

namespace sirrius.FTPFileWorker.Jobs
{
    [DisallowConcurrentExecution]
    public class FileTransferJob : IJob
    {
        private readonly IConfiguration configuration;
        private readonly IFTPConnectionSettingService FTPConnectionSettingService;
        private readonly IBankStatementService bankStatementService;
        private readonly IBankStatementTransactionService bankStatementTransactionService;
        private readonly ILogService logService;

        public FileTransferJob(IConfiguration configuration,
                               IFTPConnectionSettingService FTPConnectionSettingService,
                               IBankStatementService bankStatementService,
                               IBankStatementTransactionService bankStatementTransactionService,
                               ILogService logService)
        {
            this.configuration = configuration;
            this.FTPConnectionSettingService = FTPConnectionSettingService;
            this.bankStatementService = bankStatementService;
            this.bankStatementTransactionService = bankStatementTransactionService;
            this.logService = logService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            bool isCompanyExist = true;

            var FTPConnectionSettings = FTPConnectionSettingService.GetAll();

            var port = int.Parse(configuration.GetSection("FTPSettings:Port").Value);
            var FTPFileDownloadDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Files", "FTP");

            foreach (var FTPConnectionSetting in FTPConnectionSettings)
            {
                isCompanyExist = (FTPConnectionSetting.Company != null);
                string folderName = string.Empty;

                if (string.IsNullOrEmpty(FTPConnectionSetting.Address) ||
                    string.IsNullOrEmpty(FTPConnectionSetting.UserName) ||
                    string.IsNullOrEmpty(FTPConnectionSetting.Password))
                {
                    logService.LogError($"Missing data for this FTPConnectionSetting : ${FTPConnectionSetting.Name}, " +
                                         $"company : {(isCompanyExist ? FTPConnectionSetting.Company.Name : "Unknown")}");

                    continue;
                }

                if (!isCompanyExist)
                {
                    logService.LogError($"Missing company for FTPConnectionSetting : {FTPConnectionSetting.Name}");
                    continue;
                }

                port = FTPConnectionSetting.Port > 0 ? FTPConnectionSetting.Port : port;

                //1_metixCRM yerine her FTP sunucusunun ait oldugu company/Client adi yazilsin. Company/Client uzerinde calisilacak.
                //FTPFileDowloadDirectory = Path.Combine(FTPFileDowloadDirectory, "1_metixCRM");

                //set FTP file for requested company
                var parts = FTPConnectionSetting.Company.Name.Split(" ");

                if (parts.Length > 2)
                    folderName = FTPConnectionSetting.CompanyId + "_" + parts[0].ConvertTurkishChars().ReplaceCharSet(new char[] { '-', '/', '.', ' ' }, '_') + "_" +
                                parts[1].ConvertTurkishChars().ReplaceCharSet(new char[] { '-', '/', '.', ' ' }, '_');
                else
                    folderName = FTPConnectionSetting.CompanyId + "_" + parts[0].ConvertTurkishChars().ReplaceCharSet(new char[] { '-', '/', '.', ' ' }, '_');

                FTPFileDownloadDirectory = Path.Combine(FTPFileDownloadDirectory, folderName);

                using (var client = new SftpClient(FTPConnectionSetting.Address, port, FTPConnectionSetting.UserName, FTPConnectionSetting.Password))
                {
                    try
                    {
                        client.Connect();
                    }
                    catch (SocketException sex)
                    {
                        logService.LogError($"An error has been occured while connecting FTP server : {FTPConnectionSetting.Address}, error : {sex.Message}");
                        continue;
                    }

                    if (!client.IsConnected) continue;

                    DownloadDirectory(client, "/", FTPFileDownloadDirectory);

                    client.Disconnect();
                }

                var files = Directory.EnumerateFiles(FTPFileDownloadDirectory, "*", SearchOption.AllDirectories);

                foreach (var file in files)
                    SaveBankStatementData(file, FTPConnectionSetting.CompanyId);
            }

            return Task.CompletedTask;
        }

        private void DownloadDirectory(SftpClient sftpClient, string sourceRemotePath, string destLocalPath)
        {
            if (!Directory.Exists(destLocalPath))
                Directory.CreateDirectory(destLocalPath);
            //throw new Exception("download folder not exist");
            //Directory.Delete(destLocalPath);
            //DeleteAllFolderAndDirectories(destLocalPath);

            //Directory.CreateDirectory(destLocalPath);

            IEnumerable<SftpFile> files = sftpClient.ListDirectory(sourceRemotePath);

            foreach (SftpFile file in files)
            {
                if ((file.Name != ".") && (file.Name != ".."))
                {
                    string sourceFilePath = sourceRemotePath + "/" + file.Name;
                    string destFilePath = Path.Combine(destLocalPath, file.Name);

                    if (file.IsDirectory)
                    {
                        DownloadDirectory(sftpClient, sourceFilePath, destFilePath);
                    }
                    else
                    {
                        if (File.Exists(destFilePath)) continue;

                        using (Stream fileStream = File.Create(destFilePath))
                        {
                            sftpClient.DownloadFile(sourceFilePath, fileStream);
                            //SaveBankStatementData(destFilePath, companyId);
                        }
                    }
                }
            }
        }

        private void SaveBankStatementData(string FTPFilePath, int companyId)
        {
            if (!File.Exists(FTPFilePath))
            {
                logService.LogError($"Destination FTP file could not be found : {FTPFilePath}");
                return;
            }

            FTPBankStatement FTPBankStatement = new FTPBankStatement(FTPFilePath);

            var bankStatement = new Data.Entity.BankStatement
            {
                CompanyId = companyId,
                BankStatementDate = FTPBankStatement.closingBalance.date,
                CurrencyCode = FTPBankStatement.closingBalance.currencyCode,
                Sender = FTPBankStatement.senderReference,
                AccountNumber = FTPBankStatement.authorization,
                StatementNumber = FTPBankStatement.messageIndexTotal,
                OpeningBalance = FTPBankStatement.openingBalance.amount,
                ClosingBalance = FTPBankStatement.closingBalance.amount,
                ClosingAvailableBalance = FTPBankStatement.closingAvailableBalance != null ? FTPBankStatement.closingAvailableBalance.amount : 0,
            };

            var data = bankStatementService.IsExist(bankStatement);

            if (data.exist)
            {
                logService.LogError($"BankStatement has been already created. BankStatement ID : {data.id}");

                return;
            }

            bankStatement = bankStatementService.Create(bankStatement);

            foreach (var ft in FTPBankStatement.transactions)
            {
                bankStatementTransactionService.Create(new Data.Entity.BankStatementTransaction
                {
                    TransactionDate = ft.date,
                    BankStatementId = bankStatement.Id,
                    IsCredit = ft.isCredit,
                    CurrencyCode = ft.currencyCode,
                    Amount = ft.amount,
                    OperationType = ft.bookingType,
                    OperationCode = ft.bookingCode,
                    ClientReference = ft.receiverReference,
                    BankReference = ft.bankReference,
                    CreatedBy = 1,
                    CreatedAt = DateTime.Now,
                });
            }
        }
    }
}
