using System.Text.Json.Serialization;

namespace cred_system_back_end_app.Application.UseCase.ProviderInsurerCompanyStatus.DTO
{
    public class SetStatusInsurerDto
    {
        [JsonPropertyName("picsId")]
        public int Id { get; set; }
        public string StatusCode { get; set; }
        public string Comment { get; set; }

    }
}
