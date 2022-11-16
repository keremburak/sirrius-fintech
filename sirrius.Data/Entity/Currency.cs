using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class Currency : BaseEntity, IEntity<int>
    {
        [Required]
        public string Description { get; set; }

        public virtual ICollection<CompanyBankAccount> CompanyBankAccounts { get; set; }
    }
}
