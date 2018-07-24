using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;
using PersonalBlog.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Category.Include(c => c.ParentCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .Select(c => new { c.Id, c.Url })
                .SingleOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return RedirectToRoute("CategoryByUrl", new { url = category.Url });
        }

        public async Task<IActionResult> DetailsByUrl(string url)
        {
            var categoryViewModel = (CategoryViewModel)await _context.Category
                .Include(c => c.ParentCategory)
                .SingleOrDefaultAsync(m => m.Url == url);

            if (categoryViewModel == null)
            {
                return NotFound();
            }

            return View("Details", categoryViewModel);
        }
    }
}
