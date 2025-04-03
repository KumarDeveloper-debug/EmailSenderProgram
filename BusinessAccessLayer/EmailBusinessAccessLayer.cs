using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using BusinessAccessLayer.EmailService;
using CommonUtility;
using EmailSenderProgram;
using ErrorLogger;

namespace BusinessAccessLayer
{
    public static class EmailBusinessAccessLayer
    {
        public static bool DoWelcomeEmailWork()
        {
            var smtpClient = new SmtpClient();

            var welcomeEmailService = new WelcomeEmailService(smtpClient);
            return welcomeEmailService.SendEmail();
        }

        public static bool DoComeBackEmailWork(string voucher)
        {
            var smtpClient = new SmtpClient();

            var comeBackEmailService = new ComeBackEmailService(smtpClient);
            return comeBackEmailService.SendEmail();
        }
    }
}
