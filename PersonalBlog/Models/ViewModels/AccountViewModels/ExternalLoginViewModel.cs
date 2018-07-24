using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models.ViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
