using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models.ViewModels
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
        public ApplicationUser InsertUser { get; set; }

        [Display(Name = "کاربر ویرایش‌کننده")]
        public ApplicationUser UpdateUser { get; set; }

        [Display(Name = "کاربر حذفکننده")]
        public ApplicationUser DeleteUser { get; set; }
    }
}
