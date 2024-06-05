namespace cred_system_back_end_app.Application.DTO.Responses
{
    public class DelegateResponseDto
    {
        public int Id { get; set; }
        public DelegateTypeDto? DelegateType { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }

        public DelegateCompanyDto? DelegateCompany { get; set; }

        public bool IsActive { get; set; }

        public List<ProviderDelegateDto>? ProviderDelegate { get; set; }
    }
}
