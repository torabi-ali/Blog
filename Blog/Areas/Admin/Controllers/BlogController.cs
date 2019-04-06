using Blog.Data;
using Blog.DomainClass;
using Blog.Extensions;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Action Methods
        public async Task<IActionResult> Index(int? page = 1, string searchString = null, string category = null, int? enabled = null)
        {
            var pageSize = 25;
            var list = _context.Post.Include(p => p.Categories).AsNoTracking();

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
                list = list.Where(p => p.Title.Contains(searchString) || p.Title.Contains(saferSearchString) ||
                                  p.Summary.Contains(searchString) || p.Summary.Contains(saferSearchString));
            }

            if (category != null)
            {
                var saferBook = CommonUtility.CleanString(category);
                list = list.Where(p => p.Categories.Any(q => q.Category.Name.Contains(category) || q.Category.Name.Contains(saferBook) ||
                                  q.Category.AlternativeName.Contains(category) || q.Category.AlternativeName.Contains(saferBook)));
            }

            var model = list.OrderByDescending(x => x.InsertDateTime).Select(p => new PostViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Url = p.Url,
                ImagePath = p.ImagePath,
            });

            return View(await PaginatedList<PostViewModel>.CreateAsync(model, page ?? 1, pageSize));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            await CheckUrl(postViewModel);

            if (ModelState.IsValid)
            {
                Post post = postViewModel;
                await AddCategories(post, postViewModel.CategoryList);

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PostViewModel postViewModel = await _context.Post.Include(p => p.Categories).SingleOrDefaultAsync(m => m.Id == id);
            if (postViewModel == null)
            {
                return NotFound();
            }

            return View(postViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostViewModel postViewModel)
        {
            await CheckUrl(postViewModel);

            if (ModelState.IsValid)
            {
                var post = await _context.Post.Where(p => p.Id == postViewModel.Id).Include(p => p.Categories).FirstOrDefaultAsync();

                #region Mapping
                post.SourceName = postViewModel.SourceName;
                post.SourceUrl = postViewModel.SourceUrl;
                post.IsPin = postViewModel.IsPin;
                post.Title = postViewModel.Title;
                post.Url = postViewModel.Url;
                post.ImagePath = postViewModel.ImagePath;
                post.Summary = postViewModel.Summary;
                post.Text = postViewModel.Text;
                post.MetaDescription = postViewModel.MetaDescription;
                post.FocusKeyword = postViewModel.FocusKeyword;
                post.VisitCount = postViewModel.VisitCount;
                #endregion

                await AddCategories(post, postViewModel.CategoryList);

                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            return View(postViewModel);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var post = await _context.Post.SingleOrDefaultAsync(m => m.Id == id);
                post.DeleteUserId = currentUserId;
                _context.Post.Remove(post);

                if (await _context.SaveChangesAsync() > 0)
                    return new OkResult();
            }

            return new BadRequestResult();
        }

        [HttpPost]
        [AjaxOnly]
        public IActionResult SEO(ContentViewModel model)
        {
            if (Request.IsAjaxRequest())
            {
                SeoCheker seoChecker = new SeoCheker(model.Title, model.FocusKeyword, model.Url, model.MetaDescription, model.Text);
                seoChecker.Check();

                return Json((SEOViewModel)seoChecker);
            }

            return new BadRequestResult();
        }
        #endregion

        #region Utility Methods
        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }

        private async Task AddCategories(Post post, string categoryList)
        {
            categoryList = categoryList.NullIfEmpty();
            if (categoryList == null)
                return;

            categoryList = categoryList.Substring(0, (categoryList.Length - 1));
            var list = post.Categories.Select(p => p.CategoryId);
            foreach (var item in categoryList.Split(','))
            {
                if (list.Contains(item.TryToInt()))
                    break;

                var postCategory = new PostCategory
                {
                    Post = post,
                    Category = await _context.Category.Where(p => p.Id == item.TryToInt()).SingleOrDefaultAsync()
                };
                post.Categories.Add(postCategory);
            }
        }

        private async Task CheckUrl(PostViewModel postViewModel)
        {
            var exist = await _context.Post.AnyAsync(p => p.Url == postViewModel.Url && p.Id != postViewModel.Id);
            if (exist)
                ModelState.AddModelError("Url", "آدرس صفحه تکراری است.");
        }
        #endregion
    }
}
