using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Store.Common;
using Food_Store.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food_Store.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        ShopContext _context;
        public UsersController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.Select( x => new UserModel() {
                Email = x.Email,
                Id = x.Id,
                Role = x.Role.Name
            });
            return View(users);
        }
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Include(x => x.Role).FirstOrDefault(x => x.Id == id);
            var vievUser = new UserModel()
            {
                Email = user.Email,
                Role = user.Role.Name,
                Id = user.Id
            };

            return View(vievUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Users.FindAsync(id);
            _context.Users.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}