using Blog.Data;
using Blog.DomainClass;
using Blog.Extensions;
using Blog.Models;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Action Methods
        public async Task<IActionResult> Index(int? page = 1, string searchString = null, int? enabled = null)
        {
            var pageSize = 25;
            var list = _context.Category.Include(p => p.ParentCategory).AsNoTracking();

            #region Statistics
            var EnabledCategories = list.Where(p => p.IsEnable).Count();
            var TotalCategories = list.Count();
            float progress = (float)EnabledCategories / TotalCategories;
            int integerProgress = (int)(progress * 100);
            ViewData["Progress"] = integerProgress;
            ViewData["Enabled"] = EnabledCategories.ToString("N0");
            #endregion

            if (enabled == 1)
                list = list.Where(p => p.IsEnable);
            else if (enabled == 2)
                list = list.Where(p => !p.IsEnable);

            if (searchString != null)
            {
                var saferSearchString = CommonUtility.CleanString(searchString);
                list = list.Where(p => p.Name.Contains(searchString) || p.Name.Contains(saferSearchString) ||
                                  p.AlternativeName.Contains(searchString) || p.AlternativeName.Contains(saferSearchString));
            }

            var model = list.OrderByDescending(x => x.InsertDateTime).Select(p => new CategoryViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Url = p.Url,
                ParentCategory = p.ParentCategory,
                IsEnable = p.IsEnable,
                InsertUser = p.InsertUser,
                InsertDateTime = p.InsertDateTime
            });

            return View(await PaginatedList<CategoryViewModel>.CreateAsync(model, page ?? 1, pageSize));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            await CheckUrl(categoryViewModel);

            if (ModelState.IsValid)
            {
                if (categoryViewModel.ParentCategoryId < 0)
                    categoryViewModel.ParentCategoryId = null;

                Category category = categoryViewModel;
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryViewModel categoryViewModel = await _context.Category.Include(p => p.ParentCategory).SingleOrDefaultAsync(m => m.Id == id);
            if (categoryViewModel == null)
            {
                return NotFound();
            }
            if (categoryViewModel.ParentCategoryId == null)
            {
                categoryViewModel.ParentCategoryId = -1;
            }

            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel categoryViewModel)
        {
            await CheckUrl(categoryViewModel);

            if (ModelState.IsValid)
            {
                if (categoryViewModel.ParentCategoryId < 0)
                    categoryViewModel.ParentCategoryId = null;

                var category = await _context.Category.Where(p => p.Id == categoryViewModel.Id).FirstOrDefaultAsync();

                #region Mapping
                category.Name = category.Name;
                category.AlternativeName = category.AlternativeName;
                category.ParentCategoryId = category.ParentCategoryId;
                category.Title = categoryViewModel.Title;
                category.Url = categoryViewModel.Url;
                category.ImagePath = categoryViewModel.ImagePath;
                category.Summary = categoryViewModel.Summary;
                category.Text = categoryViewModel.Text;
                category.MetaDescription = categoryViewModel.MetaDescription;
                category.FocusKeyword = categoryViewModel.FocusKeyword;
                category.VisitCount = categoryViewModel.VisitCount;
                category.IsEnable = categoryViewModel.IsEnable;
                #endregion

                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(categoryViewModel);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var category = await _context.Category.SingleOrDefaultAsync(m => m.Id == id);
                category.DeleteUserId = currentUserId;
                _context.Category.Remove(category);

                if (await _context.SaveChangesAsync() > 0)
                    return new OkResult();
            }

            return new BadRequestResult();
        }
        #endregion

        #region Utility Methods
        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }

        [HttpGet]
        public virtual async Task<JsonResult> GetCategories()
        {
            var result = await _context.Category.OrderBy(p => p.Name).Select(p => new { id = p.Id, text = p.Name }).ToListAsync();
            return Json(result);
        }

        private async Task CheckUrl(CategoryViewModel categoryViewModel)
        {
            var exist = await _context.Category.AnyAsync(p => p.Url == categoryViewModel.Url && p.Id != categoryViewModel.Id);
            if (exist)
                ModelState.AddModelError("Url", "آدرس صفحه تکراری است.");
        }
        #endregion
    }
}
