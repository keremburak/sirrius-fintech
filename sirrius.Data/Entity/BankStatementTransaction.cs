using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class BankStatementTransaction : BaseEntity, IEntity<int>
    {
        public DateTime? TransactionDate { get; set; }
        public bool IsCredit { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
        public string OperationType { get; set; } //Swift / N - Non-Swift
        public string OperationCode { get; set; }
        public bool IsTransferred { get; set; } = false;
        public string ClientReference { get; set; }
        public string BankReference { get; set; } //document number
        //public string DocumentNumber { get { return ClientReference + "//" + BankReference; } }
        public string Description { get; set; } //86

        public int BankStatementId { get; set; }
        public BankStatement BankStatement { get; set; }
    }
}
