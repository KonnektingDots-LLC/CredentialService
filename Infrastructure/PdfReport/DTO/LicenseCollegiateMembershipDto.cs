using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class LicenseCollegiateMembershipDto
    {
        [JsonProperty("licCollegiateMemberName")]
        public string? LicCollegiateMember { get; set; }

        [JsonProperty("licCollegiateMemberLicenseNumber")]
        public string? LicCollegiateMemberLicenseNumber { get; set; }

        [JsonProperty("licCollegiateMemberExpirationDate")]
        public string? LicCollegiateMemberExpirationDate { get; set; }
    }
}
