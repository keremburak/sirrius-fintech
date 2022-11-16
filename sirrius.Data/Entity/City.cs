using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class City : BaseEntity, IEntity<int>
    {
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
