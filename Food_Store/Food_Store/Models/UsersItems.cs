using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Store.Models
{
    public class UsersItems
    {
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        public double Amount { get; set; }
        public Item Item { get; set; }
        public User User { get; set; }
    }
}
