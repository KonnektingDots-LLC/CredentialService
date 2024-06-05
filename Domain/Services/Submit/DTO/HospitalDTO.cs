namespace cred_system_back_end_app.Domain.Services.Submit.DTO
{
    public class HospitalDTO
    {
        public int HospitalListId { get; set; }
        public int HospitalPrivilegesType { get; set; }
        public int ProviderStartingMonth { get; set; }
        public int ProviderStartingYear { get; set; }
        public int ProviderEndingMonth { get; set; }
        public int ProviderEndingYear { get; set; }
        public string HospitalListOther { get; set; }
        public string HospitalPrivilegesTypeOther { get; set; }
    }

    public class HospitalAffiliationsDTO
    {
        public HospitalDTO Primary { get; set; }
        public HospitalDTO? Secondary { get; set; }
    }
}
