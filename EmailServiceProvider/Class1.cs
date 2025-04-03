using System;
using System.Net.Mail;

namespace EmailServiceProvider
{
    public class WelcomeEmailService : IEmailService
    {
        public bool SendEmail(string email)
        {
            try
            {
                //List all customers
                //If the customer is newly registered, one day back in time
                // var customers = DataLayer.ListCustomers().Where(c => c.CreatedDateTime == DateTime.Now.AddDays(-1));// TODO - check condition
                var customers = DataLayer.ListCustomers().Where(c => c.CreatedDateTime.Date == DateTime.Now.AddDays(-1).Date);
                //loop through list of new customers
                foreach (var customer in customers)
                {
                    //Create a new MailMessage
                    MailMessage mailMessage = MailMessageHelper.GetWelcomeMailMessage(customer.Email);

                    //#if DEBUG
                    //Don't send mails in debug mode, just write the emails in console
                    Console.WriteLine("Send mail to:" + customer.Email);
                    //#else
                    //Create a SmtpClient to our smtphost: yoursmtphost
                    SmtpClient smtp = SmtpClientHelper.GetSmtpClient();
                    smtp.Send(mailMessage);

                    //#endif
                }
                //All mails are sent! Success!
                return true;
            }
            catch (Exception ex)
            {
                //Log.LogError(ex, "Error sending welcome emails: " + ex.Message);

                return false;
            }
        }
    }

    public class ComeBackEmailService : IEmailService
    {
        public bool SendEmail(string email)
        {
            throw new NotImplementedException();
        }
    }

    public interface IEmailService
    {
        bool SendEmail(string email);
    }
}
