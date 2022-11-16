using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class CompanyBankAccount : BaseEntity, IEntity<int>
    {
        public string BranchCode { get; set; } //Bank branch code
        public string AccountNumber { get; set; } //company account number
        public string IBAN { get; set; } //company IBAN

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

        [NotMapped]
        public string CompanyName { get { return Company?.Name; } }
        [NotMapped]
        public string BankName { get { return Bank?.Name; } }
        [NotMapped]
        public string CurrencyName { get { return Currency?.Name; } }
        [NotMapped]
        public string Accountingcode { get { return Bank?.Name; } }
    }
}
