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
    public class ShopController : Controller
    {
        ShopContext _context;
        public ShopController (ShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string TypeString)
        {

            var dbList = _context.Items.Include(x => x.ItemType);
            var finalList = new List<ItemModel>();
            foreach (var dbItem in dbList)
            {
                finalList.Add(new ItemModel()
                {
                    Description = dbItem.Description,
                    Name = dbItem.Name,
                    Path = dbItem.Path,
                    Price = dbItem.Price,
                    Id = dbItem.Id,
                    TypeOfItem = _context.Types.FirstOrDefault(x => x.Id == dbItem.ItemType[0].ItTypeId).Name
                });
            }

            if (!String.IsNullOrEmpty(TypeString))
            {
                finalList = finalList.Where(x => x.TypeOfItem == TypeString).ToList();
            }
            return View(finalList);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public async Task<IActionResult> Buy(Guid? id)
        {

            return View();
        }
    }
}