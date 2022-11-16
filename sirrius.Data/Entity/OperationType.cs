using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class OperationType : BaseEntity, IEntity<int>
    {
        public string Description { get; set; }
    }
}
