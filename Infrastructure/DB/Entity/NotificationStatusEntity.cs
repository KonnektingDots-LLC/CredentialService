using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class NotificationStatusEntity
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        [MaxLength(100)]
        public string EmailFrom { get; set; }
        [MaxLength(100)]
        public string EmailTo { get; set; }
        [MaxLength(100)]
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        #region Relationship
        public ICollection<NotificationErrorEntity> NotificationErrors { get; set; }
        public NotificationEntity Notification { get; set; }
        #endregion
    }
}
