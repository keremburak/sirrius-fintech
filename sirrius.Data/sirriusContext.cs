using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using sirrius.Data.Entity;

namespace sirrius.Data
{
    public class sirriusContext : DbContext
    {
        public sirriusContext()
        {

        }
        public sirriusContext(DbContextOptions<sirriusContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json", optional: false);
            var config = builder.Build();
            var settingsSection = config.GetSection("ConnectionStrings:DefaultConnection");
            optionsBuilder.UseSqlServer(settingsSection.Value);

            //optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=TEGV-Dev;Persist Security Info=True;User ID=tegv;Password=Aa384we326sasa*-");

            optionsBuilder.EnableSensitiveDataLogging();

            //optionsBuilder.UseNpgsql("Host=78.135.8.83;Database=TEGV-Test1;Username=postgres;Password=metix123;Persist Security Info=True");
            //optionsBuilder.UseSqlServer("Data Source=78.135.8.83;Initial Catalog=TEGV-Test1;Persist Security Info=True;User ID=metix;Password=M3t!x123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Bank>().ToTable("Banks");
            modelBuilder.Entity<OperationCode>().ToTable("OperationCodes");
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Currency>().ToTable("Currencies");
            modelBuilder.Entity<Company>().ToTable("Companies");
            modelBuilder.Entity<CompanyUser>().ToTable("CompanyUsers");
            modelBuilder.Entity<FTPConnectionSetting>().ToTable("FTPConnectionSettings");
            modelBuilder.Entity<CompanyBankAccount>().ToTable("CompanyBankAccounts");
            modelBuilder.Entity<OperationType>().ToTable("OperationTypes");
            modelBuilder.Entity<DocumentType>().ToTable("DocumentTypes");
            modelBuilder.Entity<BankStatement>().ToTable("BankStatements");
            modelBuilder.Entity<BankStatementTransaction>().ToTable("BankStatementTransactions");
            modelBuilder.Entity<MyClientAccount>().ToTable("MyClientAccounts");
            modelBuilder.Entity<MyClientAccountingCode>().ToTable("MyClientAccountingCodes");
            modelBuilder.Entity<MyClientFTPMatching>().ToTable("MyClientFTPMatchings");
            // modelBuilder.Entity<CurrentAccount>().ToTable("CurrentAccounts");


            //modelBuilder.Entity<MenuItem>().ToTable("MenuItems");
            //modelBuilder.Entity<RoleMenuPermission>().ToTable("RoleMenuPermissions");

            modelBuilder.Entity<User>()
                        .HasOne(s => s.Role)
                        .WithMany(g => g.Users)
                        //.HasForeignKey(s => s.RoleId)
                        .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<CompanyUser>().HasKey(cu => new { cu.CompanyId, cu.UserId });
            modelBuilder.Entity<CompanyUser>()
                        .HasOne(cu => cu.Company)
                        .WithMany(c => c.CompanyUsers)
                        .HasForeignKey(cu => cu.CompanyId)
                        .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CompanyUser>()
                        .HasOne(cu => cu.User)
                        .WithMany(c => c.CompanyUsers)
                        .HasForeignKey(cu => cu.UserId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Company>()
                        .HasOne(s => s.Country)
                        .WithMany(t => t.Companies)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Company>()
                        .HasOne(s => s.Client)
                        .WithMany(t => t.Companies)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<City>()
                        .HasOne(s => s.Country)
                        .WithMany(t => t.Cities)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Company>()
                        .HasOne(s => s.Category)
                        .WithMany(t => t.Companies)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Bank>()
                        .HasOne(s => s.Country)
                        .WithMany(g => g.Banks)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FTPConnectionSetting>()
                        .HasOne(s => s.Company)
                        .WithMany(g => g.FTPConnectionSettings)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FTPConnectionSetting>()
                     .HasOne(s => s.Bank)
                     .WithMany(g => g.FTPConnectionSettings)
                     .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<CompanyBankAccount>().HasKey(cba => new { cba.CompanyId, cba.BankId, cba.CurrencyId});
            modelBuilder.Entity<CompanyBankAccount>()
                      .HasOne(cba => cba.Company)
                      .WithMany(c => c.CompanyBankAccounts)
                      .HasForeignKey(cu => cu.CompanyId)
                      .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CompanyBankAccount>()
                        .HasOne(cu => cu.Bank)
                        .WithMany(c => c.CompanyBankAccounts)
                        .HasForeignKey(cu => cu.BankId)
                        .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CompanyBankAccount>()
                        .HasOne(cu => cu.Currency)
                        .WithMany(c => c.CompanyBankAccounts)
                        .HasForeignKey(cu => cu.CurrencyId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BankStatementTransaction>()
                 .HasOne(s => s.BankStatement)
                 .WithMany(t => t.BankStatementTransactions)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MyClientFTPMatching>()
                   .HasOne(s => s.OperationCode)
                   .WithMany(g => g.MyClientFTPMatchings)
                   .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MyClientAccount>()
                        .HasOne(s => s.MyClientFTPMatching)
                        .WithOne(mm => mm.MyClientAccount)
                        .HasForeignKey<MyClientFTPMatching>(mm => mm.MyClientAccountId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MyClientAccountingCode>()
                   .HasOne(s => s.MyClientFTPMatching)
                   .WithOne(mm => mm.MyClientAccountingCode)
                   .HasForeignKey<MyClientFTPMatching>(mm => mm.MyClientAccountingCodeId)
                   .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Role>(b =>
            //{
            //    // Each Role can have many entries in the RoleMenuPermission join table
            //    b.HasMany(e => e.MenuPermissions).WithMany(e => e.Roles)
            //    .UsingEntity<RoleMenuPermission>(
            //        y => y.HasOne(y => y.MenuItem).WithMany().HasForeignKey("MenuItemId").OnDelete(DeleteBehavior.NoAction),
            //        y => y.HasOne(y => y.Role).WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.NoAction))
            //    .ToTable("RoleMenuPermissions").HasKey(y => new { y.RoleId, y.MenuItemId });
            //});

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
