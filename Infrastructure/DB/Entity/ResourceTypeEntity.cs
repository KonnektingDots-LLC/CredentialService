using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ResourceTypeEntity
    {
        [MaxLength(10), Key]
        public string Id { get; set; }

        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        #region Relationship
        public ICollection<NotificationEntity> Notifications { get; set; }
        #endregion
    }
}
