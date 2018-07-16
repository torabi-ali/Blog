using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
