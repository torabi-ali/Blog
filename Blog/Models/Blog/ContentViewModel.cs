using Blog.DomainClass;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class ContentViewModel : AuditViewModel
    {
        [Required, MaxLength(256)]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Required, MaxLength(256)]
        [Display(Name = "آدرس")]
        public string Url { get; set; }

        [MaxLength(32)]
        [Display(Name = "کلمه کلیدی")]
        public string FocusKeyword { get; set; }

        [MaxLength(512)]
        [Display(Name = "توضیحات متا")]
        public string MetaDescription { get; set; }

        [MaxLength(1024)]
        [Display(Name = "خلاصه متن")]
        public string Summary { get; set; }

        [Display(Name = "متن")]
        public string Text { get; set; }

        [MaxLength(256)]
        [Display(Name = "تصویر")]
        public string ImagePath { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int VisitCount { get; set; }


        public static implicit operator ContentViewModel(Content content)
        {
            return new ContentViewModel
            {
                Title = content.Title,
                Url = content.Url,
                ImagePath = content.ImagePath,
                Summary = content.Summary,
                Text = content.Text,
                MetaDescription = content.MetaDescription,
                FocusKeyword = content.FocusKeyword,
                VisitCount = content.VisitCount
            };
        }
    }
}
