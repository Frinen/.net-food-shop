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

namespace Food_Store.Controllers
{
    [Authorize(Roles = "admin")]
    public class ItTypesController : Controller
    {
        private readonly ShopContext _context;

        public ItTypesController(ShopContext context)
        {
            _context = context;
        }

        // GET: ItTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Types.ToListAsync());
        }

        // GET: ItTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itType = await _context.Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itType == null)
            {
                return NotFound();
            }

            return View(itType);
        }

        // GET: ItTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] ItType itType)
        {
            if (ModelState.IsValid)
            {
                itType.Id = Guid.NewGuid();
                _context.Add(itType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itType);
        }

        // GET: ItTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itType = await _context.Types.FindAsync(id);
            if (itType == null)
            {
                return NotFound();
            }
            return View(itType);
        }

        // POST: ItTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] ItType itType)
        {
            if (id != itType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItTypeExists(itType.Id))
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
            return View(itType);
        }

        // GET: ItTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itType = await _context.Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itType == null)
            {
                return NotFound();
            }

            return View(itType);
        }

        // POST: ItTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itType = await _context.Types.FindAsync(id);
            _context.Types.Remove(itType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItTypeExists(Guid id)
        {
            return _context.Types.Any(e => e.Id == id);
        }
    }
}
