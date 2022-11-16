using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class Company : BaseEntity, IEntity<int>
    {
        public string ShortName { get; set; }

        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }

        public int CityId { get; set; }
        [NotMapped]
        public string CityName { get; set; }
        public virtual City City { get; set; }

        public int CountryId { get; set; }
        //[NotMapped]
        //public string CountryName { get; set; }
        public virtual Country Country { get; set; }


        public int CategoryId { get; set; }
        [NotMapped]
        public string CategoryName { get; set; }
        public virtual Category Category { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public virtual ICollection<CompanyUser> CompanyUsers { get; set; }
        public virtual ICollection<FTPConnectionSetting> FTPConnectionSettings { get; set; }
        public virtual ICollection<CompanyBankAccount> CompanyBankAccounts { get; set; }
        public virtual ICollection<BankStatement> BankStatements { get; set; }
    }
}
