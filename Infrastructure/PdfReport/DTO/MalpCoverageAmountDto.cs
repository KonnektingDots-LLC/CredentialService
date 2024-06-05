using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class MalpCoverageAmountDto
    {
        [JsonProperty("malpPerOccurrence")]
        public string PerOccurrence { get; set; }

        [JsonProperty("malpAggregateLimit")]
        public string AggregateLimit { get; set; }
    }
}
