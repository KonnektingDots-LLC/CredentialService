using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class ProfCoverageAmountDto
    {
        [JsonProperty("profLPerOccurrence")]
        public string PerOccurrence { get; set; }

        [JsonProperty("profLAggregateLimit")]
        public string AggregateLimit { get; set; }

    }
}
