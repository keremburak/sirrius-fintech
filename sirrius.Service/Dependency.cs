using Microsoft.Extensions.DependencyInjection;
using sirrius.Core;
using sirrius.Service.Interfaces;
using sirrius.Service.Services;
using System;

namespace sirrius.Service
{
    public static class Dependency
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddSingleton<ILogService, LogService>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICompanyBankAccountService, CompanyBankAccountService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IOperationCodeService, OperationCodeService>();
            services.AddScoped<IFTPConnectionSettingService, FTPConnectionSettingService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IBankStatementService, BankStatementService>();
            services.AddScoped<IBankStatementTransactionService, BankStatementTransactionService>();
            services.AddScoped<IOperationTypeService, OperationTypeService>();
            services.AddScoped<IDocumentTypeService, DocumentTypeService>();

            services.AddScoped<IMyClientAccountingCodeService, MyClientAccountingCodeService>();
            services.AddScoped<IMyClientAccountService, MyClientAccountService>();
            services.AddScoped<IMyClientFTPMatchingService, MyClientFTPMatchingService>();

            //services.AddScoped<ICurrentAccountService, CurrentAccountService>();
        }
    }
}
