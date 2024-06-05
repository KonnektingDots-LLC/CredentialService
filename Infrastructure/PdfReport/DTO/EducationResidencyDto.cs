using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class EducationResidencyDto
    {
        [JsonProperty("eduResidency")]
        public string? EduResidency { get; set; }

        [JsonProperty("eduResidencyName")]
        public string? EduResidencyName { get; set; }

        [JsonProperty("eduResidencyAddress")]
        public string? EduResidencyAddress { get; set; }

        [JsonProperty("eduResCityStZipCode")]
        public string? EduResCityStZipCode { get; set; }

        [JsonProperty("eduResidencyFrom")]
        public string? EduResidencyFrom { get; set; }

        [JsonProperty("eduResidencyTo")]
        public string? EduResidencyTo { get; set; }

        [JsonProperty("eduResidencyType")]
        public string? EduResidencyType { get; set; }

        [JsonProperty("eduResidencyComplDt")]
        public string? EduResidencyComplDt { get; set; }

        [JsonProperty("eduResHospComplDt")]
        public string? EduResHospComplDt { get; set; }
    }
}
