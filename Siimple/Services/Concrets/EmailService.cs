using Siimple.Services.Abstracts;
using System.Net.Mail;
using System.Net;
using System.Runtime.CompilerServices;

namespace Siimple.Services.Concrets
{
    public class EmailService :IEmailConfirm
    {
        private readonly IConfiguration _configuration;
        private readonly SmtpClient _smtpClient;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Port = 587,
                Credentials = new NetworkCredential(_configuration["Email:From"], _configuration["Email:PasswordApp"])
            };
            _smtpClient = smtpClient;
        }
        public void SendMessage(string message, string subject, string to) 
        {
            MailMessage Newmessage = new MailMessage(_configuration["Email:From"], to)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            _smtpClient.Send(Newmessage);
        }
    }
}
