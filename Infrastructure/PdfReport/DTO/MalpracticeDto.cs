using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class MalpracticeDto
    {
        [JsonProperty("malpCarrierName")]
        public string? MalpCarrierName { get; set; }

        [JsonProperty("malpEffectiveFrom")]
        public string? MalpEffectiveFrom { get; set; }

        [JsonProperty("malpEffectiveTo")]
        public string? MalpEffectiveTo { get; set; }

        [JsonProperty("malpPolicyNum")]
        public string? MalpPolicyNum { get; set; }

        [JsonProperty("malpCoverageAmt")]
        public MalpCoverageAmountDto? MalpCoverageAmt { get; set; }

        [JsonProperty("malpOIGCaseNum")]
        public string? MalpOIGCaseNum { get; set; }
    }
}
