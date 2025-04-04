using System;
using System.Configuration;
using System.Net.Mail;
using System.Threading;
using BusinessAccessLayer;
using EmailDataModel;
using ErrorLogger;

namespace EmailSenderProgram
{
    internal class Program
    {
        /// <summary>
        /// This application is run everyday
        /// </summary>
        /// <param name="args"></param>

        private static int retryCount = int.Parse(ConfigurationManager.AppSettings["RetryCount"]);
        private static int retryDuration = int.Parse(ConfigurationManager.AppSettings["RetryDuration"]);
        private static int currentAttempt = int.Parse(ConfigurationManager.AppSettings["CurrentAttempt"]);
        private static void Main(string[] args)
        {

            while (true)
            {
                if (DateTime.Now.Hour == 0)
                {

                    var requestData = new RequestData();
                    requestData.Voucher = "A1000";
                    requestData.EmailSmtpClient = GetSmtpClient();

                    SendEmailWithRetry(retryCount, retryDuration, currentAttempt, requestData);
                }

                Thread.Sleep(60000); // wait for 1 minute
            }
            //Console.ReadKey();
        }

        private static void SendEmailWithRetry(int retryCount, int retryDuration, int currentAttempt, RequestData requestData)
        {
            if (currentAttempt <= retryCount)
            {
                //Call the method that do the work for me, I.E. sending the mails
                Console.WriteLine("Send Welcomemail");
                bool success = EmailBusinessAccessLayer.DoWelcomeEmailWork();

                // #if DEBUG mode, always send Comeback mail
                Console.WriteLine("Send Comebackmail");
                success = EmailBusinessAccessLayer.DoComeBackEmailWork("EOComebackToUs");
                //#else
                //Every Sunday run Comeback mail
                if (DateTime.Now.DayOfWeek.Equals(DayOfWeek.Sunday))
                {
                    Console.WriteLine("Send Comebackmail");
                    success = EmailBusinessAccessLayer.DoComeBackEmailWork("EOComebackToUs");
                }


                //Check if the sending went OK
                if (success)
                {
                    Console.WriteLine("All mails are sent, I hope...");
                }
                //Check if the sending was not going well...
                if (success == false)
                {
                    Console.WriteLine("Oops, something went wrong when sending mail (I think...)");

                    Console.WriteLine($"Retry {currentAttempt} failed: Oops, something went wrong when sending mail (I think...)");
                    if (currentAttempt < retryCount)
                    {
                        Thread.Sleep(retryDuration);
                        SendEmailWithRetry(retryCount, retryDuration, currentAttempt++ , requestData);
                    }
                    else
                    {
                        Log.LogError("Oops", "After retry three times the mail is not send to user. Something went wrong");
                    }
                }
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