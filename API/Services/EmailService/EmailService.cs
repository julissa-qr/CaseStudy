using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace API.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
            
        }

        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("christa.kessler16@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject= "ORDER CREATED";
            email.Body = new TextPart(TextFormat.Html) {Text = "Dear user your ordehas been created"};
            
            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls); //smtp.gmail.com
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);


        }
    }
}