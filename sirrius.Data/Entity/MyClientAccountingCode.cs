using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class MyClientAccountingCode : BaseEntity, IEntity<int>
    {
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public int MyClientFTPMatchingId { get; set; }
        public virtual MyClientFTPMatching MyClientFTPMatching { get; set; }

        //public virtual ICollection<MyClientFTPMatching> MyClientFTPMatchings { get; set; }

    }
}
