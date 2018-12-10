using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Store.Common;
using Microsoft.AspNetCore.Mvc;

namespace Food_Store.Controllers
{
    public class UserController : Controller
    {
        ShopContext _context;
        public UserController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}