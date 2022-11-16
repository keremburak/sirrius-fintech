using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class Role : BaseEntity, IEntity<int>
    {
        public virtual ICollection<User> Users { get; set; }

        //public virtual ICollection<MenuItem> MenuPermissions { get; set; }
    }
}
