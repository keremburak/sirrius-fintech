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
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company, int> companyRepository;
        private readonly IRepository<Country, int> countryRepository;
        private readonly IRepository<City, int> cityRepository;
        private readonly IRepository<Category, int> categoryRepository;
        private readonly IRepository<CompanyUser, int> companyUserRepository;
        private readonly IRepository<Client, int> clientRepository;

        public CompanyService(IRepository<Company, int> companyRepository,
                              IRepository<Country, int> countryRepository,
                              IRepository<City, int> cityRepository,
                              IRepository<Category, int> categoryRepository,
                              IRepository<CompanyUser, int> companyUserRepository,
                              IRepository<Client, int> clientRepository)
        {
            this.companyRepository = companyRepository;
            this.countryRepository = countryRepository;
            this.cityRepository = cityRepository;
            this.categoryRepository = categoryRepository;
            this.companyUserRepository = companyUserRepository;
            this.clientRepository = clientRepository;
        }

        public async Task<Company> CreateAsync(Company model)
        {
            return await companyRepository.InsertAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            var company = await companyRepository.FindByIdAsync(id);
            await companyRepository.DbDeleteAsync(company);
        }

        public async Task<IEnumerable<Company>> GetAllAsync(User user = null)
        {
            var companies = await companyRepository.FindAllAsync();

            if (user != null)
            {
                var Client = await clientRepository.FindAsync(q => q.UserId == user.Id);
                companies = companies.Where(q => q.ClientId == Client.Id).ToList();
            }

            return companies;
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            var company = await companyRepository.FindByIdAsync(id);

            if (company != null)
            {
                company.Country = await countryRepository.FindByIdAsync(company.CountryId);
                company.City = await cityRepository.FindByIdAsync(company.CityId);
                company.Category = await categoryRepository.FindByIdAsync(company.CategoryId);
            }

            return company;
        }

        public async Task<IEnumerable<CompanyUser>> GetCompanyUser(int companyId = 0, int userId = 0)
        {
            var companyUserList = await companyUserRepository.FindAllAsync();
            IEnumerable<CompanyUser> companyUsers = null;

            if (companyId > 0)
                companyUsers = companyUserList.Where(x => x.CompanyId == companyId);
            else if (userId > 0)
                companyUsers = companyUserList.Where(x => x.UserId == userId);

            return companyUsers;
        }

        public async Task<IEnumerable<Company>> GetClientCompaniesByClientId(int ClientId)
        {
            return await companyRepository.FindAllAsync(q => q.ClientId == ClientId);
        }

        public async Task<IEnumerable<Company>> GetClientCompaniesByUserId(int userId)
        {
            var Client = await clientRepository.FindAsync(q => q.UserId == userId);

            return await companyRepository.FindAllAsync(q => q.ClientId == Client.Id);
        }

        public async Task<(bool exist, string message)> IsExist(string text, string fieldname = "name")
        {
            string message = string.Empty;

            var companies = await GetAllAsync();

            if (fieldname.Equals("ShortName"))
                return (companies.Any(q => SirriusHelper.fullTextSearch(q.ShortName, text)), "ShortName already exist");
            else
                return (companies.Any(q => SirriusHelper.fullTextSearch(q.Name, text)), "Company Name already exist");
        }

        public async Task<Company> UpdateAsync(int id, Company model)
        {
            var company = await companyRepository.FindByIdAsync(id);


            company.Name = model.Name;
            company.ShortName = model.ShortName;
            company.PhoneNumber = model.PhoneNumber;
            company.FaxNumber = model.FaxNumber;
            company.Email = model.Email;
            company.TaxNumber = model.TaxNumber;
            company.Address = model.Address;
            company.CountryId = model.CountryId;
            company.CityId = model.CityId;
            company.CategoryId = model.CategoryId;

            return await companyRepository.UpdateAsync(company);
        }
    }
}
