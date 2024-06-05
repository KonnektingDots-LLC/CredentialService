namespace cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatus.DTO
{
    public class ProviderInsurerCompanyStatusDto
    {
        public int ProviderId { get; set; }
        public string InsurerCompanyId { get; set; }
        public string InsurerStatusTypeId { get; set; }
        public DateTime CurrentStatusDate { get; set; }
        public DateTime SubmitDate { get; set; } 
        public string CreatedBy { get; set; }

    }
}
