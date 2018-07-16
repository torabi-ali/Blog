using System.Threading.Tasks;

namespace PersonalBlog.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
