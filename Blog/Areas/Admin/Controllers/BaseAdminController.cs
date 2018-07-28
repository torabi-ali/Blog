using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    public abstract partial class BaseAdminController : Controller
    {
        internal int? currentUserId;
        protected BaseAdminController()
        {
            //currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier).TryToInt();
        }
    }
}