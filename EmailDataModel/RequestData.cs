using System.Net.Mail;

namespace EmailDataModel
{
    public class RequestData
    {
        public string Voucher { get; set; }
        public SmtpClient EmailSmtpClient { get; set; }  
    }
}
