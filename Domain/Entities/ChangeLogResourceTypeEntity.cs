using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class ChangeLogResourceTypeEntity : EntityAuditBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        #region Relationships
        public ICollection<ChangeLogEntity> ChangeLog { get; set; }
        #endregion

    }
}
