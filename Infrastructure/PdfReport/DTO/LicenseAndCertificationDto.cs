using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class LicenseAndCertificationDto
    {
        [JsonProperty("LicenseDEA")]
        public LicenseDEADto? LicenseDEA { get; set; }

        [JsonProperty("LicenseASSMCA")]
        public LicenseASSMCADto? LicenseASSMCA { get; set; }

        [JsonProperty("LicenseMedical")]
        public LicenseMedicalDto? LicenseMedical { get; set; }

        [JsonProperty("LicenseCollegiateMembership")]
        public LicenseCollegiateMembershipDto? LicenseCollegiateMembership { get; set; }

        [JsonProperty("LicensePTAN")]
        public LicensePTANDto? LicensePTAN { get; set; }

        [JsonProperty("LicenseTelemedicine")]
        public LicenseTelemedicineDto? LicenseTelemedicine { get; set; }
    }
}
