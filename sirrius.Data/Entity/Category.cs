using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class Category : BaseEntity, IEntity<int>
    {
        public virtual ICollection<Company> Companies { get; set; }

    }
}
