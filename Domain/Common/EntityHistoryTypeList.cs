namespace cred_system_back_end_app.Domain.Common
{
    public class EntityHistoryTypeList : EntityAuditBase
    {
        public bool IsExpired { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}
