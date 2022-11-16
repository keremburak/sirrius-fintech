using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class Country : BaseEntity, IEntity<int>
    {
        public string Code { get; set; }
        public bool Default { get; set; } = false;

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<City> Cities { get; set; }

        public virtual ICollection<Bank> Banks { get; set; }
    }
}
