using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Iot.Main.Infrastructure.Commands
{
    public class SendEmailCommand
    {
        private readonly IConfiguration _configuration;

        public SendEmailCommand(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMany(string message, params string[] emails)
        {
            var login = _configuration["EmailLogin"];
            var password = _configuration["EmailPassword"];
            var fromMail = _configuration["EmailFrom"];

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                foreach (var email in emails)
                {
                    var from = new MailAddress(fromMail, "Monitoring system");
                    var to = new MailAddress(email);
                    var mailMessage = new MailMessage(from, to);
                    mailMessage.Subject = "Monitoring system";
                    mailMessage.Body = message;
                    client.Credentials = new NetworkCredential(login, password);
                    await client.SendMailAsync(mailMessage);
                }
            }
        }
    }
}