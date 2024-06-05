using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class ProviderDelegateEntity : EntityAuditBase
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public ProviderEntity Provider { get; set; }
        public int DelegateId { get; set; }
        public bool IsActive { get; set; } = true;
        public DelegateEntity Delegate { get; set; }

    }
}
