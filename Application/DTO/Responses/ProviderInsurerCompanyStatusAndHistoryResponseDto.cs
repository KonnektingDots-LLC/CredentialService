using System.Text.Json.Serialization;

namespace cred_system_back_end_app.Application.DTO.Responses
{
    public class ProviderInsurerCompanyStatusAndHistoryResponseDto
    {
        [JsonPropertyName("StatusSummary")]
        public ProviderInsurerCompanyStatusResponseDto ProviderInsurerCompanyStatusResponse { get; set; }
        [JsonPropertyName("StatusHistory")]
        public List<ProviderInsurerCompanyStatusHistoryResponseDto> ProviderInsurerCompanyStatusHistoryResponse { get; set; }

    }
}
