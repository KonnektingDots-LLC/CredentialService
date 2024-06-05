using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class EducationSchoolDto
    {
        [JsonProperty("eduSchoolName")]
        public string? EduSchoolName { get; set; }

        [JsonProperty("eduSchAddress")]
        public string? EduSchAddress { get; set; }

        [JsonProperty("eduSchCityStZipCode")]
        public string? EduSchCityStZipCode { get; set; }

        [JsonProperty("eduSchGradDateFrom")]
        public string? EduSchGradDateFrom { get; set; }

        [JsonProperty("eduSchGradDateTo")]
        public string? EduSchGradDateTo { get; set; }

        [JsonProperty("eduSchSpecialty")]
        public string? EduSchSpecialty { get; set; }
    }
}
