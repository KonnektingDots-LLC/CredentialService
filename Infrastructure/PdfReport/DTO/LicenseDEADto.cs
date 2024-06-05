using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class LicenseDEADto
    {
        [JsonProperty("licDEACert")]
        public string? LicDEACert { get; set; }

        [JsonProperty("licDEALicenseNumber")]
        public string? LicDEALicenseNumber { get; set; }

        [JsonProperty("licDEAExpirationDate")]
        public string? LicDEAExpirationDate { get; set; }

    }
}
