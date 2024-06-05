namespace cred_system_back_end_app.Domain.Entities
{
    public class NotificationErrorEntity
    {
        public int Id { get; set; }
        public int NotificationStatusId { get; set; }
        public NotificationStatusEntity NotificationStatus { get; set; }
        public string ErrorDetail { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
