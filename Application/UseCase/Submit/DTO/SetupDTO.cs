namespace cred_system_back_end_app.Application.UseCase.Submit.DTO
{
    public class SetupDTO
    {
        public string ProviderEmail { get; set; }
        public int ProviderId { get; set; }
        public int Role { get; set; }
        public bool PcpApplies { get; set; }
        public bool InsuranceApplies { get; set; }
        public bool F330applies { get; set; }
        public bool HospitalAffiliationsApplies { get; set; }
    }
}
