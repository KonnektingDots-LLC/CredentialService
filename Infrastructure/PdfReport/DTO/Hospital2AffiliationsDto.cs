using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class Hospital2AffiliationsDto
    {
        [JsonProperty("hosp2Name")]
        public string? Hosp2Name { get; set; }

        [JsonProperty("hosp2PrivType")]
        public string? Hosp2PrivType { get; set; }

        [JsonProperty("hosp2PrivFrom")]
        public string? Hosp2PrivFrom { get; set; }

        [JsonProperty("hosp2PrivTo")]
        public string? Hosp2PrivTo { get; set; }
    }
}
