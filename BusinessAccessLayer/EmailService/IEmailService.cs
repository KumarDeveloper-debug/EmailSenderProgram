using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace BusinessAccessLayer.EmailService
{
    public interface IEmailService
    {
        SmtpClient EmailSmtpClient { get; set; }
        bool SendEmail();
    }
}
