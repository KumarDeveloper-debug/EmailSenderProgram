
using System.Net.Mail;

namespace BusinessAccessLayer.EmailService
{
    public interface IEmailService
    {
        SmtpClient EmailSmtpClient { get; set; }
        bool SendEmail();
    }
}
