using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Store.Common;
using Food_Store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food_Store.Controllers
{
    public class CartController : Controller
    {
        private ShopContext _context;

        public CartController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()

        {
            var userId = _context.Users.FirstOrDefault(x => x.Email == User.Identity.Name).Id;
            var dblist = _context.UsersItems
                .Include(x => x.Item)
                .ThenInclude(x => x.ItemType)
                .Where(x => x.UserId == userId);
            var list = new List<ItemBuyModel>();
            foreach(var item in dblist)
            {
               
                list.Add(new ItemBuyModel()
                {
                    Amount = item.Amount,
                    Name = item.Item.Name,
                    Path = item.Item.Path,
                    Price = item.Item.Price,
                    Description = item.Item.Description,
                    Id = item.Item.Id,
                    TypeOfItem = _context.Types.FirstOrDefault(x => x.Id == item.Item.ItemType[0].ItTypeId).Name
                });
            }
            
            return View(list);
        }
    }
}