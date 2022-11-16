using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public interface IEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }

        //public bool Deleted { get; set; }
        //public string Name { get; set; }
    }
}
