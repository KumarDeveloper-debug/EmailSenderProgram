using System.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace CommonUtility
{
    public static class MailMessageHelper
    {
        public static MailMessage GetWelcomeMailMessage(string email)
        {
            if (!IsValidEmail(email))
            {
                return null;

            }
            //Create a new MailMessage
            MailMessage mailMessage = new MailMessage();

            //Add customer to reciever list
            mailMessage.To.Add(email);
            //Add subject
            mailMessage.Subject = ConstantText.Welcome_Subject;
            //Send mail from info@EO.com
            //mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["SmtpFromEmail"]);
            mailMessage.IsBodyHtml = true;

            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["SmtpFromEmail"]);
            //Add body to mail
            mailMessage.Body = string.Format("Hi {0}<br/><br/>{1}", email, ConstantText.Welcome_Email_Body);

            return mailMessage;
        }

        public static MailMessage GeOrderMailMessage(string email)
        {
            if (!IsValidEmail(email))
            {
                return null;

            }
            //Create a new MailMessage
            MailMessage mailMessage = new MailMessage();
            //Add customer to reciever list
            mailMessage.To.Add(email);
            //Add subject
            mailMessage.Subject = ConstantText.ComeBack_Subject;
            //Send mail from info@EO.com
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["SmtpFromEmail"]);
            mailMessage.IsBodyHtml = true;
            //Add body to mail
            mailMessage.Body = string.Format("Hi {0}<br/><br/>{1}", email, ConstantText.ComeBack_Email_Body);
            return mailMessage;
        }

        private static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return emailRegex.IsMatch(email);
        }
    }
}
