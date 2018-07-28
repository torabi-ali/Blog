using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class BaseViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
    }
}