using cred_system_back_end_app.Application.UseCase.Notifications.DTO;
using cred_system_back_end_app.Infrastructure.Smtp;

namespace cred_system_back_end_app.Application.CRUD.Communication.DTO
{
    public class SmtpClientRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
