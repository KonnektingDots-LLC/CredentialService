using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class NotificationEntity
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string NotificationTypeId { get; set; }
        public NotificationTypeEntity NotificationType { get; set; }
        public string ResourceId { get; set; }
        public int NotificationStatusId { get; set; }
        public NotificationStatusEntity NotificationStatus { get; set; }
        public string CreatedBy {  get; set; }
        public DateTime CreatedDate { get; set;}
        public string ResourceTypeId { get; set; }
        public ResourceTypeEntity ResourceType { get; set; }

    }
}
