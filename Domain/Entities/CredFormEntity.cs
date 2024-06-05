using cred_system_back_end_app.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace cred_system_back_end_app.Domain.Entities
{
    public class CredFormEntity : EntityAuditBase
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Version { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string CredFormStatusTypeId { get; set; }
        public DateTime? SubmitDate { get; set; }
        public DateTime? ReSubmitDate { get; set; }

        #region relationships
        public CredFormStatusTypeEntity CredFormStatusType { get; set; }
        public ProviderEntity Provider { get; set; }
        #endregion

        #region Using a different name when storing because of naming mismatch (TODO: refactor columns to be the same in all tables)
        [Column("CreatedDate")]
        public new DateTime? CreationDate { get; set; } = DateTime.Now;
        #endregion
    }
}
