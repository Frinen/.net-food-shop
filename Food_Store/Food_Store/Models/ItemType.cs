using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Store.Models
{
    public class ItemType
    {
        public Guid ItemId { get; set; }
        public Guid ItTypeId { get; set; }
        public Item Item { get; set; }
        public ItType Type { get; set; }
    }
}
