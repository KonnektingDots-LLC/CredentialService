using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatus.DTO;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatusHistory.DTO;
using System.Text.Json.Serialization;

namespace cred_system_back_end_app.Application.UseCase.ProviderInsurerCompanyStatus.DTO
{
    public class ProviderInsurerCompanyStatusAndHistoryResponseDto
    {
        [JsonPropertyName("StatusSummary")]
        public ProviderInsurerCompanyStatusResponseDto ProviderInsurerCompanyStatusResponse { get; set; }
        [JsonPropertyName("StatusHistory")]
        public List<ProviderInsurerCompanyStatusHistoryResponseDto> ProviderInsurerCompanyStatusHistoryResponse { get; set; }

    }
}
