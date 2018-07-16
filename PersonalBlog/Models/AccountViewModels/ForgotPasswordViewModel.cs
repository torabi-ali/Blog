using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
