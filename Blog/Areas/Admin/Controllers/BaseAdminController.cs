using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Blog.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    public abstract partial class BaseAdminController : Controller
    {
        internal int? currentUserId;
        protected BaseAdminController()
        {
            //currentUserId = Int32.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}