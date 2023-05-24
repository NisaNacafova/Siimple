using Siimple.Services.Abstracts;
using System.Net.Mail;
using System.Net;

namespace Siimple.Services.Concrets
{
    public class EmailService :IEmailConfirm
    {
        public void Email(string email,string link) 
        {
            MailMessage message = new MailMessage("7lz9g6x@code.edu.az", email)
            {
                Subject = "Confirmation Email",
                Body = link,
            };
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Port = 587,
                Credentials = new NetworkCredential("7lz9g6x@code.edu.az", "aoyhmyjwzdiprgyq")
            };
            smtpClient.Send(message);
        }
    }
}
