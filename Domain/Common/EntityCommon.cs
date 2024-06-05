namespace cred_system_back_end_app.Domain.Common
{
    public class EntityCommon : EntityAuditBase
    {
        public bool IsActive { get; set; } = true;
    }
}
