using System.Net.Mail;

namespace MailValidation.Verify.SMTP
{
    internal class SmtpResponse
    {
        public string Raw { get; set; }
        public SmtpStatusCode Code { get; set; }

    }
}
