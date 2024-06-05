using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class Hospital1AffiliationsDto
    {
        [JsonProperty("hosp1Name")]
        public string? Hosp1Name { get; set; }

        [JsonProperty("hosp1PrivType")]
        public string? Hosp1PrivType { get; set; }

        [JsonProperty("hosp1PrivFrom")]
        public string? Hosp1PrivFrom { get; set; }

        [JsonProperty("hosp1PrivTo")]
        public string? Hosp1PrivTo { get; set; }
    }
}
