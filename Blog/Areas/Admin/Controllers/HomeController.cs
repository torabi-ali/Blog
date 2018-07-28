using Blog.Data;
using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Diagnostics;
using System.Linq;

namespace Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;
        private IMemoryCache _cache;

        public HomeController(ApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _cache = memoryCache;
        }

        public IActionResult Index()
        {
            string[] cachedData = CacheGetOrCreate().Split(',');

            ViewData["TodayPost"] = cachedData[0];
            ViewData["TodayComment"] = cachedData[1];
            ViewData["TodayCategory"] = cachedData[2];
            ViewData["TodayUser"] = cachedData[3];

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string CacheGetOrCreate()
        {
            var cacheEntry = _cache.GetOrCreate(CacheKeys.Entry, entry =>
            {
                string cachedData;
                var today = DateTime.Today;
                string[] data = new string[4];

                data[0] = _context.Post.Where(p => (p.InsertDateTime > today || p.UpdateDateTime > today)).Count().ToString();
                data[1] = _context.Comments.Where(p => (p.InsertDateTime > today || p.InsertDateTime > today)).Count().ToString();
                data[2] = _context.Category.Where(p => (p.InsertDateTime > today || p.UpdateDateTime > today)).Count().ToString();
                data[3] = "0";// _context.Users.Where(p => p.RegistrationDate > today).Count().ToString();
                cachedData = string.Join(',', data);

                entry.SetSlidingExpiration(TimeSpan.FromMinutes(5)).SetAbsoluteExpiration(TimeSpan.FromHours(1));
                return cachedData;
            });

            return cacheEntry;
        }
    }
}
