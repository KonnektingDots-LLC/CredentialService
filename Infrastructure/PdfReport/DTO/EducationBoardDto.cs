using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class EducationBoardDto
    {
        [JsonProperty("brdCertification")]
        public string? BrdCertification { get; set; }

        [JsonProperty("brdSpecialty")]
        public string? BrdSpecialty { get; set; }

        [JsonProperty("brdSpecialtyFrom")]
        public string? BrdSpecialtyFrom { get; set; }

        [JsonProperty("brdSpecialtyTo")]
        public string? BrdSpecialtyTo { get; set; }
    }
}
