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
    public class FTPConnectionSettingService : IFTPConnectionSettingService
    {
        private readonly IRepository<FTPConnectionSetting, int> FTPSettingRepository;
        private readonly IRepository<Company, int> companyRepository;
        private readonly IRepository<Bank, int> bankRepository;

        public FTPConnectionSettingService(IRepository<FTPConnectionSetting, int> FTPSettingRepository,
                                           IRepository<Company, int> companyRepository,
                                           IRepository<Bank, int> bankRepository)
        {
            this.FTPSettingRepository = FTPSettingRepository;
            this.companyRepository = companyRepository;
            this.bankRepository = bankRepository;
        }

        public async Task<FTPConnectionSetting> CreateAsync(FTPConnectionSetting model)
        {
            return await FTPSettingRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var FTPSetting = await FTPSettingRepository.FindByIdAsync(id);
            await FTPSettingRepository.DbDeleteAsync(FTPSetting);
        }

        public IEnumerable<FTPConnectionSetting> GetAll()
        {
            var FTPConnectionSettings = FTPSettingRepository.FindAll();
            var companies = companyRepository.FindAll();
            Company company = null;

            foreach (var ftpCS in FTPConnectionSettings)
            {
                company = companies.FirstOrDefault(q => q.Id == ftpCS.CompanyId);

                if (!Equals(company, null)) ftpCS.Company = company;
            }

            return FTPConnectionSettings;
        }

        public async Task<IEnumerable<FTPConnectionSetting>> GetAllAsync()
        {
            return await FTPSettingRepository.FindAllAsync();
        }

        public async Task<FTPConnectionSetting> GetByIdAsync(int id)
        {
            var FTPConnectionSetting = await FTPSettingRepository.FindByIdAsync(id);
            FTPConnectionSetting.Company = await companyRepository.FindByIdAsync(FTPConnectionSetting.CompanyId);
            FTPConnectionSetting.Bank = await bankRepository.FindByIdAsync(FTPConnectionSetting.BankId);
            return FTPConnectionSetting;
        }

        public async Task<(bool exist, string message)> IsExist(string text, string fieldname = "host")
        {
            string message = string.Empty;

            var FTPConnectionSettings = await GetAllAsync();

            if (fieldname.Equals("host"))
                return (FTPConnectionSettings.Any(q => SirriusHelper.fullTextSearch(q.Host, text)), "FTP connection host already exist");
            else
                return (FTPConnectionSettings.Any(q => SirriusHelper.fullTextSearch(q.Address, text)), "FTP connection address already exist");
        }

        public async Task<FTPConnectionSetting> UpdateAsync(int id, FTPConnectionSetting model)
        {
            var FTPConnectionSetting = await FTPSettingRepository.FindByIdAsync(id);

            FTPConnectionSetting.Host = model.Host;
            FTPConnectionSetting.Address = model.Address;
            FTPConnectionSetting.Port = model.Port;
            FTPConnectionSetting.UserName = model.UserName;
            FTPConnectionSetting.Password = model.Password;
            FTPConnectionSetting.BankId = model.BankId;
            FTPConnectionSetting.CompanyId = model.CompanyId;
            FTPConnectionSetting.UpdatedBy = model.UpdatedBy;
            FTPConnectionSetting.UpdatedAt = model.UpdatedAt;
            FTPConnectionSetting.Name = model.Name;

            return await FTPSettingRepository.UpdateAsync(FTPConnectionSetting);
        }
    }
}
