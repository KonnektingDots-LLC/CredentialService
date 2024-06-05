using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class EducationFellowshipDto
    {
        [JsonProperty("eduFellowship")]
        public string? EduFellowship { get; set; }

        [JsonProperty("eduFellowshipName")]
        public string? EduFellowshipName { get; set; }

        [JsonProperty("eduFellowshipAddres")]
        public string? EduFellowshipAddres { get; set; }

        [JsonProperty("eduFellowshipCity")]
        public string? EduFellowshipCity { get; set; }

        [JsonProperty("eduFellowshipFrom")]
        public string? EduFellowshipFrom { get; set; }

        [JsonProperty("eduFellowshipTo")]
        public string? EduFellowshipTo { get; set; }

        [JsonProperty("eduFellowshipType")]
        public string? EduFellowshipType { get; set; }

        [JsonProperty("eduFellowshipComplDt")]
        public string? EduFellowshipComplDt { get; set; }
    }
}
