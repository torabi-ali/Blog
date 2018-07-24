using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models.ViewModels
{
    public class BaseViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
    }
}