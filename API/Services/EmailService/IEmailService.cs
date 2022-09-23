using API.DTOs;

namespace API.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}