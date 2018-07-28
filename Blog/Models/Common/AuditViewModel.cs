using Blog.DomainClass;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public abstract class AuditViewModel : BaseViewModel
    {
        [Display(Name = "زمان اضافه شدن")]
        public DateTime? InsertDateTime { get; set; }

        [Display(Name = "زمان ویرایش")]
        public DateTime? UpdateDateTime { get; set; }

        [Display(Name = "زمان حذف شدن")]
        public DateTime? DeleteDateTime { get; set; }

        [Display(Name = "حذف شذه؟")]
        public bool IsDelete { get; set; }

        [Display(Name = "فعال است؟")]
        public bool IsEnable { get; set; }

        [Display(Name = "کاربر واردکننده")]
        public IdentityUser InsertUser { get; set; }

        [Display(Name = "کاربر ویرایش‌کننده")]
        public IdentityUser UpdateUser { get; set; }

        [Display(Name = "کاربر حذفکننده")]
        public IdentityUser DeleteUser { get; set; }
    }
}
