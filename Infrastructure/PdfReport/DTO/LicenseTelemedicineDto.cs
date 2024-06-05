using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class LicenseTelemedicineDto
    {
        [JsonProperty("LicenseTelemedicineCert")]
        public string? LicTelemedicine { get; set; }

        [JsonProperty("LicenseTelemedicineLicenseNumber")]
        public string? LicTelemedicineLicenseNumber { get; set; }

        [JsonProperty("LicenseTelemedicineExpirationDate")]
        public string? LicTelemedicineExpirationDate { get; set; }
    }
}
