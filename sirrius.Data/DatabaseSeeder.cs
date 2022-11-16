using Microsoft.EntityFrameworkCore;
using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace sirrius.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Client data

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    FirstName = "Kerem",
                    LastName = "Burak",
                    PhoneNumber = "111223344",
                    Email = "kerem@test.com",
                    UserId = 2
                },
                new Client
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "111223344",
                    Email = "john@test.com",
                    UserId = 3
                });

            #endregion

            #region role data 

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "SuperAdmin",
                    Active = true,
                    Deleted = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Id = 2,
                    Name = "Admin",
                    Active = true,
                    Deleted = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Id = 3,
                    Name = "User",
                    Active = true,
                    Deleted = false,
                    CreatedAt = DateTime.UtcNow
                });

            #endregion

            #region user data

            //user data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "admin",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@sirrius.net",
                    RoleId = 1,
                    PasswordHash = BCryptNet.HashPassword("123456")
                },
                new User
                {
                    Id = 2,
                    UserName = "kerem",
                    FirstName = "Kerem",
                    LastName = "Burak",
                    Email = "kerem@test.com",
                    Active = true,
                    Deleted = false,
                    RoleId = 1,
                    CreatedBy = 1,
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = BCryptNet.HashPassword("123456")
                },
                new User
                {
                    Id = 3,
                    UserName = "john",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@test.com",
                    RoleId = 2,
                    PasswordHash = BCryptNet.HashPassword("654321")
                },
                new User
                {
                    Id = 4,
                    UserName = "jane",
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane@test.com",
                    RoleId = 3,
                    PasswordHash = BCryptNet.HashPassword("654321")
                });

            #endregion

            #region country data

            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Code = "TR", Name = "Türkiye", Default = true },
                new Country { Id = 2, Code = "CT", Name = "KKTC" });

            #endregion

            #region city data

            //city data
            modelBuilder.Entity<City>().HasData(
                    new City { Id = 1, CountryId = 1, Name = "ADANA" },
                    new City { Id = 2, CountryId = 1, Name = "ADIYAMAN" },
                    new City { Id = 3, CountryId = 1, Name = "AFYON" },
                    new City { Id = 4, CountryId = 1, Name = "AĞRI" },
                    new City { Id = 5, CountryId = 1, Name = "AMASYA" },
                    new City { Id = 6, CountryId = 1, Name = "ANKARA" },
                    new City { Id = 7, CountryId = 1, Name = "ANTALYA" },
                    new City { Id = 8, CountryId = 1, Name = "ARTVİN" },
                    new City { Id = 9, CountryId = 1, Name = "AYDIN" },
                    new City { Id = 10, CountryId = 1, Name = "BALIKESİR" },
                    new City { Id = 11, CountryId = 1, Name = "BİLECİK" },
                    new City { Id = 12, CountryId = 1, Name = "BİNGÖL" },
                    new City { Id = 13, CountryId = 1, Name = "BİTLİS" },
                    new City { Id = 14, CountryId = 1, Name = "BOLU" },
                    new City { Id = 15, CountryId = 1, Name = "BURDUR" },
                    new City { Id = 16, CountryId = 1, Name = "BURSA" },
                    new City { Id = 17, CountryId = 1, Name = "ÇANAKKALE" },
                    new City { Id = 18, CountryId = 1, Name = "ÇANKIRI" },
                    new City { Id = 19, CountryId = 1, Name = "ÇORUM" },
                    new City { Id = 20, CountryId = 1, Name = "DENİZLİ" },
                    new City { Id = 21, CountryId = 1, Name = "DİYARBAKIR" },
                    new City { Id = 22, CountryId = 1, Name = "EDİRNE" },
                    new City { Id = 23, CountryId = 1, Name = "ELAZIĞ" },
                    new City { Id = 24, CountryId = 1, Name = "ERZİNCAN" },
                    new City { Id = 25, CountryId = 1, Name = "ERZURUM" },
                    new City { Id = 26, CountryId = 1, Name = "ESKİŞEHİR" },
                    new City { Id = 27, CountryId = 1, Name = "GAZİANTEP" },
                    new City { Id = 28, CountryId = 1, Name = "GİRESUN" },
                    new City { Id = 29, CountryId = 1, Name = "GÜMÜŞHANE" },
                    new City { Id = 30, CountryId = 1, Name = "HAKKARİ" },
                    new City { Id = 31, CountryId = 1, Name = "HATAY" },
                    new City { Id = 32, CountryId = 1, Name = "ISPARTA" },
                    new City { Id = 33, CountryId = 1, Name = "İÇEL" },
                    new City { Id = 34, CountryId = 1, Name = "İSTANBUL" },
                    new City { Id = 35, CountryId = 1, Name = "İZMİR" },
                    new City { Id = 36, CountryId = 1, Name = "KARS" },
                    new City { Id = 37, CountryId = 1, Name = "KASTAMONU" },
                    new City { Id = 38, CountryId = 1, Name = "KAYSERİ" },
                    new City { Id = 39, CountryId = 1, Name = "KIRKLARELİ" },
                    new City { Id = 40, CountryId = 1, Name = "KIRŞEHİR" },
                    new City { Id = 41, CountryId = 1, Name = "KOCAELİ" },
                    new City { Id = 42, CountryId = 1, Name = "KONYA" },
                    new City { Id = 43, CountryId = 1, Name = "KÜTAHYA" },
                    new City { Id = 44, CountryId = 1, Name = "MALATYA" },
                    new City { Id = 45, CountryId = 1, Name = "MANİSA" },
                    new City { Id = 46, CountryId = 1, Name = "KAHRAMANMARAŞ" },
                    new City { Id = 47, CountryId = 1, Name = "MARDİN" },
                    new City { Id = 48, CountryId = 1, Name = "MUĞLA" },
                    new City { Id = 49, CountryId = 1, Name = "MUŞ" },
                    new City { Id = 50, CountryId = 1, Name = "NEVŞEHİR" },
                    new City { Id = 51, CountryId = 1, Name = "NİĞDE" },
                    new City { Id = 52, CountryId = 1, Name = "ORDU" },
                    new City { Id = 53, CountryId = 1, Name = "RİZE" },
                    new City { Id = 54, CountryId = 1, Name = "SAKARYA" },
                    new City { Id = 55, CountryId = 1, Name = "SAMSUN" },
                    new City { Id = 56, CountryId = 1, Name = "SİİRT" },
                    new City { Id = 57, CountryId = 1, Name = "SİNOP" },
                    new City { Id = 58, CountryId = 1, Name = "SİVAS" },
                    new City { Id = 59, CountryId = 1, Name = "TEKİRDAĞ" },
                    new City { Id = 60, CountryId = 1, Name = "TOKAT" },
                    new City { Id = 61, CountryId = 1, Name = "TRABZON" },
                    new City { Id = 62, CountryId = 1, Name = "TUNCELİ" },
                    new City { Id = 63, CountryId = 1, Name = "ŞANLIURFA" },
                    new City { Id = 64, CountryId = 1, Name = "UŞAK" },
                    new City { Id = 65, CountryId = 1, Name = "VAN" },
                    new City { Id = 66, CountryId = 1, Name = "YOZGAT" },
                    new City { Id = 67, CountryId = 1, Name = "ZONGULDAK" },
                    new City { Id = 68, CountryId = 1, Name = "AKSARAY" },
                    new City { Id = 69, CountryId = 1, Name = "BAYBURT" },
                    new City { Id = 70, CountryId = 1, Name = "KARAMAN" },
                    new City { Id = 71, CountryId = 1, Name = "KIRIKKALE" },
                    new City { Id = 72, CountryId = 1, Name = "BATMAN" },
                    new City { Id = 73, CountryId = 1, Name = "ŞIRNAK" },
                    new City { Id = 74, CountryId = 1, Name = "BARTIN" },
                    new City { Id = 75, CountryId = 1, Name = "ARDAHAN" },
                    new City { Id = 76, CountryId = 1, Name = "IĞDIR" },
                    new City { Id = 77, CountryId = 1, Name = "YALOVA" },
                    new City { Id = 78, CountryId = 1, Name = "KARABÜK" },
                    new City { Id = 79, CountryId = 1, Name = "KİLİS" },
                    new City { Id = 80, CountryId = 1, Name = "OSMANİYE" },
                    new City { Id = 81, CountryId = 1, Name = "DÜZCE" });

            #endregion

            #region category data

            modelBuilder.Entity<Category>().HasData(
                    new Category { Id = 1, Name = "Altın Mücevherat" },
                    new Category { Id = 2, Name = "Ambalaj" },
                    new Category { Id = 3, Name = "Ayakkabı" },
                    new Category { Id = 4, Name = "Boya" },
                    new Category { Id = 5, Name = "Demir Çelik" },
                    new Category { Id = 6, Name = "Deri ve Deri Mamülleri" },
                    new Category { Id = 7, Name = "Elektrikli Aletler" },
                    new Category { Id = 8, Name = "Tekstil" },
                    new Category { Id = 9, Name = "Gemi İnşa" },
                    new Category { Id = 10, Name = "Denizcilik" },
                    new Category { Id = 11, Name = "Halı İmalatı" },
                    new Category { Id = 12, Name = "Gıda" },
                    new Category { Id = 13, Name = "Hazır Giyim" },
                    new Category { Id = 14, Name = "Kimya" },
                    new Category { Id = 15, Name = "Oto Ana/Yan Sanayi" },
                    new Category { Id = 16, Name = "Temizlik Maddeleri" },
                    new Category { Id = 17, Name = "İlaç Sanayi" },
                    new Category { Id = 18, Name = "Madencilik" },
                    new Category { Id = 19, Name = "Tarım Aletleri" },
                    new Category { Id = 20, Name = "İnsaat Malzemeleri" },
                    new Category { Id = 21, Name = "Mobilya" },
                    new Category { Id = 22, Name = "Bilişim" },
                    new Category { Id = 23, Name = "Turizm" },
                    new Category { Id = 24, Name = "Catering" },
                    new Category { Id = 25, Name = "Taşımacılık" });

            #endregion

            #region company data

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "SirriUS Ltd.Sti",
                    ShortName = "SLS",
                    PhoneNumber = "0262-5551055",
                    FaxNumber = "",
                    TaxNumber = "12345678",
                    CountryId = 1,
                    CityId = 34,
                    CategoryId = 22,
                    Email = "info@sirrius.net",
                    Address = "Gebze",
                    CreatedBy = 1,
                    ClientId = 1
                },
                new Company
                {
                    Id = 2,
                    Name = "Samba Bilişim A.Ş",
                    ShortName = "SBA",
                    PhoneNumber = "0262-456465",
                    FaxNumber = "",
                    TaxNumber = "12345678",
                    CountryId = 1,
                    CityId = 41,
                    CategoryId = 22,
                    Email = "sambga@sirrius.net",
                    Address = "Kocaeli",
                    CreatedBy = 1,
                    ClientId = 2
                }
                );

            #endregion

            #region company-user data

            modelBuilder.Entity<CompanyUser>().HasData(
                  new CompanyUser { Id = 1, CompanyId = 1, UserId = 1 },
                  new CompanyUser { Id = 2, CompanyId = 1, UserId = 2 });

            #endregion

            #region FTPConnectionSetting data - will be removed in production

            modelBuilder.Entity<FTPConnectionSetting>().HasData(
                  new FTPConnectionSetting
                  {
                      Id = 1,
                      Name = "Rebex Tiny FTP Server",
                      Host = "Rebex Host",
                      Address = "192.168.56.1",
                      Port = 22,
                      UserName = "tester",
                      Password = "password",
                      CreatedBy = 1,
                      CreatedAt = DateTime.UtcNow,
                      CompanyId = 1,
                      BankId = 1
                  });

            #endregion

            #region bank data

            modelBuilder.Entity<Bank>().HasData(
                new Bank
                {
                    Id = 1,
                    Code = "0001",
                    Name = "T.C. MERKEZ BANKASI",
                    CountryId = 1,
                    BICCode = string.Empty,
                    CreatedAt = DateTime.UtcNow
                },
                new Bank
                {
                    Id = 2,
                    Code = "0004",
                    Name = "İLLER BANKASI A.Ş.",
                    CountryId = 1,
                    BICCode = string.Empty,
                    CreatedAt = DateTime.UtcNow
                },
                new Bank
                {
                    Id = 3,
                    Code = "0010",
                    Name = "T.C.ZİRAAT BANKASI A.Ş.",
                    CountryId = 1,
                    BICCode = "TCZBTR2A",
                    CreatedAt = DateTime.UtcNow
                },
                new Bank
                {
                    Id = 4,
                    Code = "0012",
                    Name = "T.HALK BANKASI A.Ş.",
                    CountryId = 1,
                    BICCode = "TRHBTR2A",
                    CreatedAt = DateTime.UtcNow
                },
                new Bank
                {
                    Id = 5,
                    Code = "0014",
                    Name = "T.SINAİ KALKINMA BANKASI A.Ş.",
                    CountryId = 1,
                    BICCode = "TSKBTRIS",
                    CreatedAt = DateTime.UtcNow
                },
                new Bank
                {
                    Id = 6,
                    Code = "0015",
                    Name = "T.VAKIFLAR BANKASI T.A.O",
                    CountryId = 1,
                    BICCode = "TVBATR2A",
                    CreatedAt = DateTime.UtcNow
                },
                new Bank
                {
                    Id = 7,
                    Code = "0016",
                    Name = "T.EXİMBANK",
                    CountryId = 1,
                    BICCode = "TIKBTR2A",
                    CreatedAt = DateTime.UtcNow
                }, new Bank
                {
                    Id = 8,
                    Code = "0017",
                    Name = "T.KALKINMA BANKASI A.Ş.",
                    CountryId = 1,
                    BICCode = "TKBNTR2A",
                    CreatedAt = DateTime.UtcNow
                },
                new Bank
                {
                    Id = 9,
                    Code = "0029",
                    Name = "BİRLEŞİK FON BANKASI A.Ş.",
                    CountryId = 1,
                    BICCode = "BAYDTRIS",
                    CreatedAt = DateTime.UtcNow
                },
                new Bank
                {
                    Id = 10,
                    Code = "0032",
                    Name = "T.EKONOMİ BANKASI A.Ş.",
                    CountryId = 1,
                    BICCode = "TEBUTRIS",
                    CreatedAt = DateTime.UtcNow
                },
                new Bank
                {
                    Id = 11,
                    Code = "0046",
                    Name = "AKBANK T.A.Ş.",
                    CountryId = 1,
                    BICCode = "AKBKTRIS",
                    CreatedAt = DateTime.UtcNow
                },
                  new Bank
                  {
                      Id = 12,
                      Code = "0059",
                      Name = "ŞEKERBANK T.A.Ş.",
                      CountryId = 1,
                      BICCode = "SEKETR2A",
                      CreatedAt = DateTime.UtcNow
                  },
                  new Bank
                  {
                      Id = 13,
                      Code = "0062",
                      Name = "T.GARANTİ BANKASI A.Ş.",
                      CountryId = 1,
                      BICCode = "TGBATRIS",
                      CreatedAt = DateTime.UtcNow
                  },
                  new Bank
                  {
                      Id = 14,
                      Code = "0064",
                      Name = "T.İŞ BANKASI A.Ş.",
                      CountryId = 1,
                      BICCode = "ISBKTRIS",
                      CreatedAt = DateTime.UtcNow
                  },
                  new Bank
                  {
                      Id = 15,
                      Code = "0067",
                      Name = "YAPI VE KREDİ BANKASI A.Ş.",
                      CountryId = 1,
                      BICCode = "YAPITRISFEX",
                      CreatedAt = DateTime.UtcNow
                  },
                  new Bank
                  {
                      Id = 16,
                      Code = "0088",
                      Name = "The Royal Bank of Scotland N.V.",
                      CountryId = 1,
                      BICCode = "ABNATRIS",
                      CreatedAt = DateTime.UtcNow
                  },
                  new Bank
                  {
                      Id = 17,
                      Code = "0091",
                      Name = "Arap Türk Bankası A.Ş.",
                      CountryId = 1,
                      BICCode = "ATUBTRIS",
                      CreatedAt = DateTime.UtcNow
                  },
                  new Bank
                  {
                      Id = 18,
                      Code = "0092",
                      Name = "Citibank A.Ş.",
                      CountryId = 1,
                      BICCode = "CITITRIX",
                      CreatedAt = DateTime.UtcNow
                  },
                  new Bank
                  {
                      Id = 19,
                      Code = "0094",
                      Name = "YBank Mellat",
                      CountryId = 1,
                      BICCode = "BKMTTRIS",
                      CreatedAt = DateTime.UtcNow
                  },
                  new Bank
                  {
                      Id = 20,
                      Code = "0096",
                      Name = "Turkish Bank A.Ş.",
                      CountryId = 1,
                      BICCode = "TUBATRIS",
                      CreatedAt = DateTime.UtcNow
                  },
                  new Bank
                  {
                      Id = 21,
                      Code = "0097",
                      Name = "Habib Bank Limited",
                      CountryId = 1,
                      BICCode = "HABBTRIS",
                      CreatedAt = DateTime.UtcNow
                  },
                  new Bank
                  {
                      Id = 22,
                      Code = "0098",
                      Name = "JPMorgan Chase Bank N.A.",
                      CountryId = 1,
                      BICCode = "CHASTRIS",
                      CreatedAt = DateTime.UtcNow
                  },
                  new Bank
                  {
                      Id = 23,
                      Code = "0099",
                      Name = "ING Bank A.Ş.",
                      CountryId = 1,
                      BICCode = "INGBTRIS",
                      CreatedAt = DateTime.UtcNow
                  },
                  new Bank
                  {
                      Id = 24,
                      Code = "00100",
                      Name = "Adabank A.Ş.",
                      CountryId = 1,
                      BICCode = "ADABTRIS",
                      CreatedAt = DateTime.UtcNow
                  },
                  new Bank
                  {
                      Id = 25,
                      Code = "00103",
                      Name = "Fibabanka A.Ş.",
                      CountryId = 1,
                      BICCode = "FBHLTRIS",
                      CreatedAt = DateTime.UtcNow
                  },
                   new Bank
                   {
                       Id = 26,
                       Code = "00106",
                       Name = "WestLB AG",
                       CountryId = 1,
                       BICCode = "WELATRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 27,
                       Code = "00108",
                       Name = "Turkland Bank A.Ş.",
                       CountryId = 1,
                       BICCode = "TBNKTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 28,
                       Code = "00109",
                       Name = "Tekstil Bankası A.Ş.",
                       CountryId = 1,
                       BICCode = "TEKBTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 29,
                       Code = "00111",
                       Name = "Finans Bank A.Ş.",
                       CountryId = 1,
                       BICCode = "FNNBTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 30,
                       Code = "00115",
                       Name = "Deutsche Bank A.Ş.",
                       CountryId = 1,
                       BICCode = "YAPITRISFEX",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 31,
                       Code = "00116",
                       Name = "Taib Yatırım Bank A.Ş.",
                       CountryId = 1,
                       BICCode = "TAIBTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 32,
                       Code = "00121",
                       Name = "Credit Agricole Yatırım Bankası Türk A.Ş.",
                       CountryId = 1,
                       BICCode = "BSUITRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 33,
                       Code = "00122",
                       Name = "Societe Generale",
                       CountryId = 1,
                       BICCode = "SOGETRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 34,
                       Code = "00123",
                       Name = "HSBC Bank A.Ş.",
                       CountryId = 1,
                       BICCode = "HSBCTRIX",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 35,
                       Code = "00124",
                       Name = "Alternatif Bank A.Ş.",
                       CountryId = 1,
                       BICCode = "ALFBTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 36,
                       Code = "00125",
                       Name = "Eurobank Tekfen A.Ş.",
                       CountryId = 1,
                       BICCode = "TEKFTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 37,
                       Code = "00129",
                       Name = "Merrill Lynch Yatırım Bank A.Ş.",
                       CountryId = 1,
                       BICCode = "MLMBIE2XIST",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 38,
                       Code = "00132",
                       Name = "İMKB Takas ve Saklama Bankası A.Ş.",
                       CountryId = 1,
                       BICCode = "TVABTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 39,
                       Code = "00134",
                       Name = "Denizbank A.Ş.",
                       CountryId = 1,
                       BICCode = "DENITRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 40,
                       Code = "00135",
                       Name = "Anadolubank A.Ş.",
                       CountryId = 1,
                       BICCode = "ANDLTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 41,
                       Code = "00138",
                       Name = "Diler Yatırım Bankası A.Ş.",
                       CountryId = 1,
                       BICCode = "DYAKTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 42,
                       Code = "00139",
                       Name = "GSD Yatırım Bankası A.Ş.",
                       CountryId = 1,
                       BICCode = "GSDBTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 43,
                       Code = "00141",
                       Name = "Nurol Yatırım Bankası A.Ş.",
                       CountryId = 1,
                       BICCode = "NUROTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 44,
                       Code = "00142",
                       Name = "BankPozitif Kredi ve Kalkınma Bankası A.Ş.",
                       CountryId = 1,
                       BICCode = "BPTRTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 45,
                       Code = "00143",
                       Name = "Aktif Yatırım Bankası A.Ş.",
                       CountryId = 1,
                       BICCode = "CAYTTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 46,
                       Code = "00205",
                       Name = "Kuveyt Türk Katılım Bankası A.Ş.",
                       CountryId = 1,
                       BICCode = "KTEFTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 47,
                       Code = "00206",
                       Name = "Türkiye Finans Katılım Bankası A.Ş.",
                       CountryId = 1,
                       BICCode = "AFKBTRIS",
                       CreatedAt = DateTime.UtcNow
                   },
                   new Bank
                   {
                       Id = 48,
                       Code = "00208",
                       Name = "Asya Katılım Bankası A.Ş.",
                       CountryId = 1,
                       BICCode = "ASYATRIS",
                       CreatedAt = DateTime.UtcNow
                   });

            #endregion

            #region bank operation code data

            modelBuilder.Entity<OperationCode>().HasData(
                new OperationCode
                {
                    Id = 1,
                    Code = "COC",
                    Name = "KREDİ KARTI KOMİSYONU",
                    CreatedAt = DateTime.UtcNow
                },
                  new OperationCode
                  {
                      Id = 2,
                      Code = "DOM",
                      Name = "TEMİNAT MEKTUP KOMİSYONU",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 3,
                      Code = "DDB",
                      Name = "MÜŞTERİ HESABINDAN",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 4,
                      Code = "EFT",
                      Name = "GELEN EFT",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 5,
                      Code = "EXP",
                      Name = "İHRACAT TAHSİLATI",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 6,
                      Code = "FEX",
                      Name = "DÖVİZ SATIŞ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 7,
                      Code = "FND",
                      Name = "STOPAJ ÜZERİNDEN",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 8,
                      Code = "IFN",
                      Name = "YATIRIM FONU SATIŞI",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 9,
                      Code = "IMC",
                      Name = "İTHALAT MASRAFI",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 10,
                      Code = "IMP",
                      Name = "İTHALAT ÖDEMESİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 11,
                      Code = "INS",
                      Name = "REPO GELİRİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 12,
                      Code = "INT",
                      Name = "FAİZ GELİRİ VADELİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 13,
                      Code = "LDP",
                      Name = "KREDİ AÇILIŞI",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 14,
                      Code = "MSC",
                      Name = "DİĞER İŞLEMLER",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 15,
                      Code = "PRO",
                      Name = "KARŞILIKSIZ SENET PROTESTOSU",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 16,
                      Code = "RDP",
                      Name = "KREDİ AÇILIŞ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 17,
                      Code = "SEC",
                      Name = "REPO GERİ DÖNÜŞ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 18,
                      Code = "SLR",
                      Name = "TOPLU MAAŞ ÖDEME",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 19,
                      Code = "SSK",
                      Name = "SSK TAHSİLATLARI",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 20,
                      Code = "STM",
                      Name = "DAMGA VERGİSİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 21,
                      Code = "STX",
                      Name = "ÖZEL İŞEM VERGİSİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 22,
                      Code = "SUF",
                      Name = "KKDF ÖDEMESİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 23,
                      Code = "TAX",
                      Name = "STOPAJ KESİNTİSİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 24,
                      Code = "TDP",
                      Name = "VADELİ HESAP KAPANIŞI",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 25,
                      Code = "TRF",
                      Name = "GELEN HAVALE",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 26,
                      Code = "TXP",
                      Name = "VERGİ TAHSİLATLARI",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 27,
                      Code = "VRG",
                      Name = "DİĞER VERGİ TAHSİLATLARI",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 28,
                      Code = "VRM",
                      Name = "GELEN VİRMAN",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 29,
                      Code = "WHT",
                      Name = "VADELİ HESAP VERGİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 30,
                      Code = "ZRH",
                      Name = "NAKİT TOPLAMA - TAHSİLAT",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 31,
                      Code = "CCP",
                      Name = "KREDİ KARTI  PEŞİNSATIŞ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 32,
                      Code = "CHG",
                      Name = "DİĞER BANKA MASRAFLARI",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 33,
                      Code = "BNK",
                      Name = "BANKA ÜCRETLERİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 34,
                      Code = "BOE",
                      Name = "SENET",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 35,
                      Code = "BRF",
                      Name = "ARACILIK ÜCRETİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 36,
                      Code = "CHK",
                      Name = "ÇEKLER",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 37,
                      Code = "CLR",
                      Name = "NAKİT / ÇEK HAVALESİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 38,
                      Code = "CMP",
                      Name = "TAZMİNAT TALEPLERİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 39,
                      Code = "COL",
                      Name = "TAHSİLATLAR",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 40,
                      Code = "COM",
                      Name = "KOMİSYON",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 41,
                      Code = "CPN",
                      Name = "KUPON ÖDEMELERİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 42,
                      Code = "DCR",
                      Name = "BELGESEL KREDİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 43,
                      Code = "DDT",
                      Name = "OTOMATİK ÖDEME ÖĞESİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 44,
                      Code = "DIS",
                      Name = "KAZANÇ ÖDEMESİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 45,
                      Code = "DIV",
                      Name = "TEMETTÜLER",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 46,
                      Code = "DKK",
                      Name = "EŞDEĞER MİKTAR",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 47,
                      Code = "EQA",
                      Name = "KENDİ HESABINA HARİCİ TRANSFER",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 48,
                      Code = "MAR",
                      Name = "TEMİNAT ÖDEMELERİ/MAKBUZLAR",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 49,
                      Code = "MAT",
                      Name = "VADE",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 50,
                      Code = "MGT",
                      Name = "YÖNETİM ÜCRETLERİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 51,
                      Code = "NWI",
                      Name = "YENİ İHRAÇLARIN DAĞITIMI",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 52,
                      Code = "ODC",
                      Name = "FAZLA ÇEKİM ÜCRETİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 53,
                      Code = "PCH",
                      Name = "SATIN ALMA (STIF VE VADELİ MEVDUAT DAHİL)",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 54,
                      Code = "PRN",
                      Name = "ANAPARA ÖDEMESİ/ÖDEMESİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 55,
                      Code = "REC",
                      Name = "VERGİ İADESİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 56,
                      Code = "RTI",
                      Name = "İADE EDİLEN ÜRÜN",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 57,
                      Code = "STP",
                      Name = "DAMGA VERGİSİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 58,
                      Code = "SWP",
                      Name = "SWAP ÖDEMESİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 59,
                      Code = "TCK",
                      Name = "SEYAHAT ÇEKLERİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 60,
                      Code = "TCM",
                      Name = "ÜÇLÜ TEMİNAT YÖNETİMİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 61,
                      Code = "TRA",
                      Name = "KENDİ HESABINA DAHİLİ TRANSFER",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 62,
                      Code = "TRN",
                      Name = "İŞLEM ÜCRETİ",
                      CreatedAt = DateTime.UtcNow
                  }, new OperationCode
                  {
                      Id = 63,
                      Code = "UWC",
                      Name = "SİGORTA KOMİSYONU",
                      CreatedAt = DateTime.UtcNow
                  });

            #endregion

            #region currency data

            modelBuilder.Entity<Currency>().HasData(
                new Currency
                {
                    Id = 1,
                    Name = "TRY",
                    Description = "Türk Lirası"
                },
                new Currency
                {
                    Id = 2,
                    Name = "USD",
                    Description = "Dolar"
                },
                new Currency
                {
                    Id = 3,
                    Name = "EUR",
                    Description = "Euro"
                },
                new Currency
                {
                    Id = 4,
                    Name = "GBP",
                    Description = "Sterlin"
                });

            #endregion

            modelBuilder.Entity<MyClientAccount>().HasData(
                new MyClientAccount { Id = 1, Code = "100.100.101", Name = "XYZ Izolasyon LTD STI", CompanyId = 2 },
                new MyClientAccount { Id = 2, Code = "100.100.102", Name = "MDF Sunta LTD STI", CompanyId = 2 },
                new MyClientAccount { Id = 3, Code = "200.100.103", Name = "Bakiroglu Yapi LTD STI", CompanyId = 2 },
                new MyClientAccount { Id = 4, Code = "200.100.104", Name = "Adatepe Koll.Ltd", CompanyId = 2 }
                );

            modelBuilder.Entity<MyClientAccountingCode>().HasData(
                new MyClientAccountingCode { Id = 1, Code = "300.100.101", Name = "Aktifler", CompanyId = 2 },
                new MyClientAccountingCode { Id = 2, Code = "400.100.102", Name = "KDV", CompanyId = 2 },
                new MyClientAccountingCode { Id = 3, Code = "500.100.103", Name = "Cariler", CompanyId = 2 },
                new MyClientAccountingCode { Id = 4, Code = "600.100.104", Name = "Giderler", CompanyId = 2 }
            );


            //modelBuilder.Entity<MenuItem>().HasData(
            //    new MenuItem { Id = 1, ShowOnSeed = true, DisplayOrder = 1, Parent = true, ParentId = 0, Title = "Dashboard", Text = "Dashboard", Href = "/", I18n = "", Route = "", Icon = "", Disabled = false, Ajax = false },
            //    new MenuItem { Id = 2, ShowOnSeed = true, DisplayOrder = 2, Parent = true, ParentId = 0, Title = "Bankalar", Text = "Bankalar", Href = "/", I18n = "", Route = "", Icon = "", Disabled = false, Ajax = false },
            //    new MenuItem { Id = 3, ShowOnSeed = true, DisplayOrder = 3, Parent = true, ParentId = 0, Title = "Banka Kodları", Text = "Banka Kodları", Href = "/", I18n = "", Route = "", Icon = "", Disabled = false, Ajax = false }
            //);
        }
    }
}
