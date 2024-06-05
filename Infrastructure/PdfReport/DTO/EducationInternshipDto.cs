using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class EducationInternshipDto
    {
        [JsonProperty("eduInternshipName")]
        public string? EduInternshipName { get; set; }

        [JsonProperty("eduInternshipAddress")]
        public string? EduInternshipAddress { get; set; }

        [JsonProperty("eduInternshipFrom")]
        public string? EduInternshipFrom { get; set; }

        [JsonProperty("eduInternshipTo")]
        public string? EduInternshipTo { get; set; }

        [JsonProperty("eduIntProgramType")]
        public string? EduIntProgramType { get; set; }
    }
}
