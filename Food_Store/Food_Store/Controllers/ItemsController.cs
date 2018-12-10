using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Food_Store.Common;
using Food_Store.Models;
using Microsoft.AspNetCore.Authorization;
using Food_Store.ViewModels;

namespace Food_Store.Controllers
{
    [Authorize(Roles = "admin")]
    public class ItemsController : Controller
    {
        private readonly ShopContext _context;

        public ItemsController(ShopContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(string TypeString)
        {
            
            var dbList = _context.Items.Include(x => x.ItemType);
            var finalList = new List<ItemModel>();
            foreach (var dbItem in dbList )
            {
                finalList.Add(new ItemModel()
                {
                    Description = dbItem.Description,
                    Name = dbItem.Name,
                    Path = dbItem.Path,
                    Price = dbItem.Price,
                    Id = dbItem.Id,
                    TypeOfItem = _context.Types.FirstOrDefault(x=> x.Id == dbItem.ItemType[0].ItTypeId).Name 
                });
            }

            if (!String.IsNullOrEmpty(TypeString))
            {
                finalList = finalList.Where(x => x.TypeOfItem == TypeString).ToList();
            }
            return View(finalList);
        }

        // GET: Items/Details/5
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

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Description,Path,Id,TypeOfItem")] ItemModel itemModel)
        {
            var item = new Item();
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid();
                item.Description = itemModel.Description;
                item.Name = itemModel.Name;
                item.Path = itemModel.Path;
                item.Price = itemModel.Price;
                
                _context.Items.Add(item);
                await _context.SaveChangesAsync();

                var itemType = new ItemType();
                itemType.ItemId = item.Id;
                itemType.ItTypeId = _context.Types.FirstOrDefault(x => x.Name == itemModel.TypeOfItem).Id;
                _context.itemTypes.Add(itemType);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Price,Description,Path,Id")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(Guid id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
    public class MyListTable
    {
        public string Key { get; set; }
        public string Display { get; set; }
    }
}
