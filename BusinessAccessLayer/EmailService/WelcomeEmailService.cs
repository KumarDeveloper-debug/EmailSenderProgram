using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using CommonUtility;
using EmailSenderProgram;
using ErrorLogger;

namespace BusinessAccessLayer.EmailService
{
    public class WelcomeEmailService : IEmailService
    {
        private readonly Customer _customer;

        public SmtpClient EmailSmtpClient { get; set; }

        public WelcomeEmailService(SmtpClient smtpClient)
        {
            EmailSmtpClient = smtpClient;
        }
        public bool SendEmail()
        {

            try
            {
                //List all customers
                //If the customer is newly registered, one day back in time
                var customers = DataLayer.ListCustomers().Where(c => c.CreatedDateTime.Date == DateTime.Now.AddDays(-1).Date);
                //loop through list of new customers
                //Create a SmtpClient to our smtphost: yoursmtphost
                SmtpClient smtp = GetSmtpClient();
                foreach (var customer in customers)
                {
                    Customer _customer = customer;

                    //Create a new MailMessage
                    MailMessage mailMessage = MailMessageHelper.GetWelcomeMailMessage(customer.Email);

                    //#if DEBUG
                    //Don't send mails in debug mode, just write the emails in console
                    Console.WriteLine("Send mail to:" + customer.Email);
                    //#else
                  
                    smtp.Send(mailMessage);

                }
                //All mails are sent! Success!
                return true;
            }
            catch (Exception ex)
            {
                Log.LogError(ex, $"Error sending welcome email to {_customer.Email}: {ex.Message}");
                return false;
            }
        }

        public static SmtpClient GetSmtpClient()
        {
            string smtpServer = ConfigurationManager.AppSettings["HostName"];

            SmtpClient smtpClient = new SmtpClient
            {
                Host = smtpServer,
                Port = int.Parse(ConfigurationManager.AppSettings["Port"]),
                EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential(
                    ConfigurationManager.AppSettings["SenderEmail"],
                    ConfigurationManager.AppSettings["SenderPassword"])
            };

            return smtpClient;
        }


    }
}
