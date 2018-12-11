using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Store.ViewModels
{
    public class ItemBuyModel : BaseModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string TypeOfItem { get; set; }
        public double Amount { get; set; }
    }
}
