using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Store.Models
{
    public class User : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid? RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<UsersItems> UsersItems { get; set; }
    }
}
