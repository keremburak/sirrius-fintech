using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class User : BaseEntity, IEntity<int>
    {
        //public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        [NotMapped]
        public string RoleName { get; set; }

        [NotMapped]
        public string FullName { get; set; }

        public virtual ICollection<CompanyUser> CompanyUsers { get; set; }
    }
}
