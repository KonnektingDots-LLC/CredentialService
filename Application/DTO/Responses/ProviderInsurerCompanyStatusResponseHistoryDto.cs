using System.Text.Json.Serialization;

namespace cred_system_back_end_app.Application.DTO.Responses
{
    public class ProviderInsurerCompanyStatusHistoryResponseDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int ProviderInsurerCompanyStatusId { get; set; }
        public string InsurerStatusTypeId { get; set; }
        public string StatusDate { get; set; }
        public string? Comment { get; set; }
        public string CommentDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
