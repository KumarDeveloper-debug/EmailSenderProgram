using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace CommonUtility
{
    public class SmtpClientHelper
    {
        public static SmtpClient GetSmtpClient()
        {
            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Host = ConfigurationManager.AppSettings["SmtpServer"];
            smtpClient.Port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            smtpClient.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"]);
            smtpClient.Credentials = new System.Net.NetworkCredential(
                ConfigurationManager.AppSettings["SmtpUsername"],
                ConfigurationManager.AppSettings["SmtpPassword"]);

            return smtpClient;
        }
    }
}

