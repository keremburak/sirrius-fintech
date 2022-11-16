using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class Client : BaseEntity, IEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }

        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
