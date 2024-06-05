using cred_system_back_end_app.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace cred_system_back_end_app.Domain.Entities
{
    public class ProviderInsurerCompanyStatusHistoryEntity : EntityAuditBase
    {
        public int Id { get; set; }
        public int ProviderInsurerCompanyStatusId { get; set; }
        public string InsurerStatusTypeId { get; set; }
        public DateTime StatusDate { get; set; }
        public string? Comment { get; set; }
        public DateTime? CommentDate { get; set; }

        #region relationships

        public ProviderInsurerCompanyStatusEntity ProviderInsurerCompanyStatus { get; set; }
        public InsurerStatusTypeEntity InsurerStatusType { get; set; }

        #endregion

        #region Using a different name when storing because of naming mismatch (TODO: refactor columns to be the same in all tables)
        [Column("CreatedDate")]
        public new DateTime? CreationDate { get; set; } = DateTime.Now;
        #endregion
    }
}
