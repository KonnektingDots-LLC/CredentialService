namespace cred_system_back_end_app.Infrastructure.Settings
{
    public class SmtpSettings
    {

        public string Host { get; set; } = "smtp.office365.com";
        public int Port { get; set; } = 587;
        public string SmtpPass { get; set; }
        public string Username { get; set; }
        public string SenderName { get; set; } = "Cred System App";
        public string SenderEmail { get; set; }

    }

}
