namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ProviderMedicalGroupEntity:RecordHistory
    {
        public int ProviderId { get; set; }
        public ProviderEntity Provider { get; set; }
        public int MedicalGroupId { get; set; }
        public MedicalGroupEntity MedicalGroup { get; set; }
    }
}
