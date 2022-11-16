using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class BankStatement : BaseEntity, IEntity<int>
    {
        public DateTime BankStatementDate { get; set; }
        public string Sender { get; set; } // tag 20 
        public string AccountNumber { get; set; } //tag 25
        public string StatementNumber { get; set; } //tag 28/28F
        public decimal OpeningBalance { get; set; }  //tag 60F
        public decimal ClosingBalance { get; set; }  //tag 60F
        public decimal ClosingAvailableBalance { get; set; }  //tag 62F
        public string CurrencyCode { get; set; } // tag 20 

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public virtual ICollection<BankStatementTransaction> BankStatementTransactions { get; set; }
    }
}
