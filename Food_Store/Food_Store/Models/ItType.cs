using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Store.Models
{
    public class ItType : BaseModel
    {
        public string Name { get; set; }
        public List<ItemType> ItemType { get; set; }
    }
}
