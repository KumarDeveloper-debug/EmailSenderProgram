using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using CommonUtility;
using EmailSenderProgram;
using ErrorLogger;

namespace BusinessAccessLayer.EmailService
{
    public class ComeBackEmailService : IEmailService
    {
        private readonly Customer _customer;

        public SmtpClient EmailSmtpClient { get; set; }

        public ComeBackEmailService(SmtpClient smtpClient)
        {
            EmailSmtpClient = smtpClient;
        }
        public bool SendEmail()
        {
            try
            {
                //List all customers 
                List<Customer> customers = DataLayer.ListCustomers();
                //List all orders
                List<Order> orders = DataLayer.ListOrders();
                SmtpClient smtp = WelcomeEmailService.GetSmtpClient();

                //loop through list of customers
                foreach (Customer customer in customers)
                {
                    Customer _customer = customer;

                    // We send mail if customer hasn't put an order
                    bool Send = true;
                    //loop through list of orders to see if customer don't exist in that list
                    Send = !orders.Any(o => o.CustomerEmail == customer.Email);

                    //Send if customer hasn't put order
                    if (Send)
                    {
                        // Create a new MailMessage
                        MailMessage mailMessage = MailMessageHelper.GeOrderMailMessage(customer.Email);
                        if (mailMessage != null)
                        {
                            // Don't send mails in debug mode, just write the emails in console
                            smtp.Send(mailMessage);
                        }
                        else
                        {
                            Log.LogError(("Invalid email address"), $"Invalid email address: {customer.Email}");
                        }
                    }
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
    }
}
