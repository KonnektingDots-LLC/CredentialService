using cred_system_back_end_app.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace cred_system_back_end_app.Domain.Entities
{
    public class CredFormStatusTypeEntity : EntityAuditBase
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? StateTypeId { get; set; }
        public int? PrioritySorting { get; set; }

        #region relationships

        public ICollection<CredFormEntity>? CredForms { get; set; }
        public StateTypeEntity? StateType { get; set; }

        #endregion

        #region Using a different name when storing because of naming mismatch (TODO: refactor columns to be the same in all tables)
        [Column("CreatedDate")]
        public new DateTime? CreationDate { get; set; } = DateTime.Now;
        #endregion
    }
}
