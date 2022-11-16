using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class Bank : BaseEntity, IEntity<int>
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; } = "";
        public string BICCode { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<FTPConnectionSetting> FTPConnectionSettings { get; set; }
        public virtual ICollection<CompanyBankAccount> CompanyBankAccounts { get; set; }
    }
}
