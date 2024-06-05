namespace cred_system_back_end_app.Infrastructure.Smtp
{
    public class SmtpClientRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
}
