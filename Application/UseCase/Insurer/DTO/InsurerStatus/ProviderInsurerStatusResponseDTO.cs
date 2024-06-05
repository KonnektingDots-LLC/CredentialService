using cred_system_back_end_app.Application.Common.ResponseDTO;

namespace cred_system_back_end_app.Application.UseCase.Insurer.DTO.InsurerStatus
{
    public class PaginatedProviderInsurerStatusResponseDTO : PaginatedResponseBaseDTONonListContent<ProviderInsurerStatusResponseDTO>
    {

    }

    public class ProviderInsurerStatusResponseDTO 
    {
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Surname { get; set; }
        public string RenderingNPI { get; set; }
        public Summary Summary { get; set; }
        public IEnumerable<ProviderInsurerStatusDTO> InsurerStatusList { get; set; }
    }

    public class ProviderInsurerStatusDTO 
    {
        public string Name { get; set; }
        public string CurrentStatusDate { get; set; }
        public string Status { get; set; }
        public string? Note { get; set; }
        public string? NoteDate { get; set; }
    }

    public class Summary 
    {
        public string LastSubmitDate { get; set; }
    }
}
