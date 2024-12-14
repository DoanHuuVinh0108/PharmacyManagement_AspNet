using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using System.IO;
using MailKit.Security;

namespace PharmacyManagermentSystem.Services.MiniServiceEmail
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string? DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class EmailService : IEmailService
    {
        private readonly MailSettings _settings;

        public EmailService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var Message = new MimeMessage();
            Message.Sender = new MailboxAddress(_settings.DisplayName, _settings.Mail);
            Message.From.Add(new MailboxAddress(_settings.DisplayName, _settings.Mail));
            Message.To.Add(MailboxAddress.Parse(email));
            Message.Subject = subject;

            var builder = new BodyBuilder()
            {
                HtmlBody = htmlMessage
            };

            Message.Body = builder.ToMessageBody();

            using (var smtp = new MailKit.Net.Smtp.SmtpClient(
                )) // Fully qualified name to resolve ambiguity
            {
                try
                {
                    await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(_settings.Mail, _settings.Password);
                    await smtp.SendAsync(Message);
                }
                catch (Exception ex)
                {
                    Directory.CreateDirectory("MailsSave");
                    var emailsavefile = string.Format(@"MailsSave/{0}.txt", email + Guid.NewGuid());
                    Console.WriteLine(ex);
                    await Message.WriteToAsync(emailsavefile);
                    await File.AppendAllTextAsync(emailsavefile, ex.Message);
                }
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
