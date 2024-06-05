using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class JsonProviderFormEntity : EntityAuditBase
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public bool IsCompleted { get; set; }
        public string JsonBody { get; set; }
    }
}
