using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ColoradoLuxury.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _host;
        private readonly int _port;
        private readonly bool _ssl;
        private readonly string _username;
        private readonly string _password;
        private readonly string _subject;
        private readonly BufferBlock<MimeMessage> mailmessage;

        public EmailSender(string host, int port, bool ssl, string username, string password, string subject)
        {
            _host = host;
            _port = port;
            _ssl = ssl;
            _username = username;
            _password = password;
            mailmessage = new BufferBlock<MimeMessage>();
            _subject = subject;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var messages = new MimeMessage();
            messages.To.Add(new MailboxAddress(email, email));
            messages.From.Add(new MailboxAddress(_subject, _username));
            messages.Subject = subject;
            messages.Body = new TextPart("html")
            {
                Text = htmlMessage
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(_host, _port, _ssl);
                smtpClient.Authenticate(_username, _password);
                await smtpClient.SendAsync(messages);
                smtpClient.Dispose();
            }
            await mailmessage.SendAsync(messages);
        }
    }
}
