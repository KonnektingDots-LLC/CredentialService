using cred_system_back_end_app.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cred_system_back_end_app.Domain.Entities
{
    public class NotificationEntity : EntityAuditBase
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string NotificationTypeId { get; set; }
        public NotificationTypeEntity NotificationType { get; set; }
        public string ResourceId { get; set; }
        public int NotificationStatusId { get; set; }
        public NotificationStatusEntity NotificationStatus { get; set; }
        public string ResourceTypeId { get; set; }
        public ResourceTypeEntity ResourceType { get; set; }

        #region Using a different name when storing because of naming mismatch (TODO: refactor columns to be the same in all tables)
        [Column("CreatedDate")]
        public new DateTime? CreationDate { get; set; } = DateTime.Now;
        #endregion

        #region hidden from data mapping
        [NotMapped]
        public new string? ModifiedBy { get; set; }
        [NotMapped]
        public new DateTime? ModifiedDate { get; set; }
        #endregion
    }
}
