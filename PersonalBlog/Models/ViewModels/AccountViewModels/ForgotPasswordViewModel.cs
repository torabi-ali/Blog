using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
