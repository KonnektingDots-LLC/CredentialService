using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class ProviderMedicalGroupEntity : EntityCommon
    {
        public int ProviderId { get; set; }
        public ProviderEntity Provider { get; set; }
        public int MedicalGroupId { get; set; }
        public MedicalGroupEntity MedicalGroup { get; set; }
    }
}
