using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class FTPConnectionSetting : BaseEntity, IEntity<int>
    {
        public string Host { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
