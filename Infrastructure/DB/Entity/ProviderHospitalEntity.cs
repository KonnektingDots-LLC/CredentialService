namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ProviderHospitalEntity:RecordHistory
    {
        public int ProviderId { get; set; }
        public int HospitalId { get; set; }

        public ProviderEntity Provider { get; set; }
        public HospitalEntity Hospital { get; set; }
    }
}
