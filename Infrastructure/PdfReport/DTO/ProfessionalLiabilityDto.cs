using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class ProfessionalLiabilityDto
    {
        [JsonProperty("profLCarrierName")]
        public string? ProfLCarrierName { get; set; }

        [JsonProperty("profLPolicyNum")]
        public string? ProfLPolicyNum { get; set; }

        [JsonProperty("profLCoverageAmt")]
        public ProfCoverageAmountDto? ProfLCoverageAmt { get; set; }

        [JsonProperty("profLEffectiveFrom")]
        public string? ProfLEffectiveFrom { get; set; }

        [JsonProperty("profLEffectiveTo")]
        public string? ProfLEffectiveTo { get; set; }
    }
}
