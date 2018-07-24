using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Data;
using PersonalBlog.Models.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;

namespace PersonalBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var today = DateTime.Today;

            ViewData["TodayCategory"] = _context.Category.Where(p => (p.InsertDateTime > today || p.UpdateDateTime > today)).Count();
            ViewData["TodayPost"] = _context.Post.Where(p => (p.InsertDateTime > today || p.UpdateDateTime > today)).Count();
            ViewData["TodayUser"] = _context.Users.Where(p => p.RegistrationDate > today).Count();

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
