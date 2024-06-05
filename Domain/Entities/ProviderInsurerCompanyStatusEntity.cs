using cred_system_back_end_app.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace cred_system_back_end_app.Domain.Entities
{
    public class ProviderInsurerCompanyStatusEntity : EntityAuditBase
    {
        public int Id { get; set; }
        public string InsurerStatusTypeId { get; set; }
        public int ProviderId { get; set; }
        public string InsurerCompanyId { get; set; }
        public DateTime CurrentStatusDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public string? Comment { get; set; }
        public DateTime? CommentDate { get; set; }

        #region relationships

        public InsurerStatusTypeEntity InsurerStatusType { get; set; }
        public ProviderEntity Provider { get; set; }
        public InsurerCompanyEntity InsurerCompany { get; set; }
        public ICollection<ProviderInsurerCompanyStatusHistoryEntity> ProviderInsurerCompanySatusHistory { get; set; }

        #endregion

        #region Using a different name when storing because of naming mismatch (TODO: refactor columns to be the same in all tables)
        [Column("CreatedDate")]
        public new DateTime? CreationDate { get; set; } = DateTime.Now;
        #endregion
    }
}
