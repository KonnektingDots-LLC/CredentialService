using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class ProviderHospitalEntity : EntityCommon
    {
        public int ProviderId { get; set; }
        public int HospitalId { get; set; }

        public ProviderEntity Provider { get; set; }
        public HospitalEntity Hospital { get; set; }
    }
}
