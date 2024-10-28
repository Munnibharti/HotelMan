
using System.Net.Mail;
using System.Net;

namespace HotelManagementProject.Service
{
    public class EmailServices : IEmailServices
    {
        private readonly SmtpClient _smtpClient;

        public EmailServices()
        {
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("umabharti56789@gmail.com", "Manku@1234"),
                EnableSsl = true,
            };
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("umabharti56789@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}

