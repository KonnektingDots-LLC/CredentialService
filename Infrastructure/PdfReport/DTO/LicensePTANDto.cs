using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class LicensePTANDto
    {
        [JsonProperty("licPTANProgram")]
        public string? LicPTANCProgram { get; set; }

        [JsonProperty("licPTANLicenseNumber")]
        public string? LicPTANLicenseNumber { get; set; }

        [JsonProperty("licPTANExpirationDate")]
        public string? LicPTANExpirationDate { get; set; }
    }
}
