using cred_system_back_end_app.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace cred_system_back_end_app.Domain.Entities
{
    public class NotificationProfileCompletionDetailEntity : EntityAuditBase
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool Sent { get; set; }

        #region hidden from data mapping
        [NotMapped]
        public new string? CreatedBy { get; set; }
        [NotMapped]
        public new DateTime? CreationDate { get; set; }
        [NotMapped]
        public new string? ModifiedBy { get; set; }
        [NotMapped]
        public new DateTime? ModifiedDate { get; set; }
        #endregion
    }
}
