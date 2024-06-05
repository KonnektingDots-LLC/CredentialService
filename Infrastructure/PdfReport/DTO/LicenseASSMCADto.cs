using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class LicenseASSMCADto
    {
        [JsonProperty("licASSMCALicense")]
        public string? LicASSMCACert { get; set; }

        [JsonProperty("licASSMCALicenseNumber")]
        public string? LicASSMCALicenseNumber { get; set; }

        [JsonProperty("licASSMCAExpirationDate")]
        public string? LicASSMCAExpirationDate { get; set; }
    }
}
