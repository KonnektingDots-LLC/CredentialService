using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class LicenseMedicalDto
    {
        [JsonProperty("licMedicalLicense")]
        public string? LicMedicalLicense { get; set; }

        [JsonProperty("licMedicalLicenseNumber")]
        public string? LicMedicalLicenseNumber { get; set; }

        [JsonProperty("licMedicalExpirationDate")]
        public string? LicMedicalExpirationDate { get; set; }
    }
}
