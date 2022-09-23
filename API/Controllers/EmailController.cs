using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace API.Controllers
{
    public class EmailController : BaseApiController
    {
       
        [HttpPost]
        public IActionResult SendEmail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("christa.kessler16@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("christa.kessler16@ethereal.email"));
            email.Subject= "Test Email Subject";
            email.Body = new TextPart(TextFormat.Html) {Text = body};

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls); //smtp.gmail.com
            smtp.Authenticate("christa.kessler16@ethereal.email", "RC4D37EyGBxEKHKuvd");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();

        }

    }
}