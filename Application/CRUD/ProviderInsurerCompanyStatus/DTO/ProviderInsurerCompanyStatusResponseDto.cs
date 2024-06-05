using System.Text.Json.Serialization;

namespace cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatus.DTO
{
    public class ProviderInsurerCompanyStatusResponseDto
    {
        [JsonPropertyName("picsId")]
        public int Id { get; set; }
        public string InsurerStatusTypeId { get; set; }
        public int ProviderId { get; set; }
        public string InsurerCompanyId { get; set; }
        public string CurrentStatusDate { get; set; }
        public string SubmitDate { get; set; }
        public string? Comment { get; set; }
        public string CommentDate { get; set; }
    }
}
