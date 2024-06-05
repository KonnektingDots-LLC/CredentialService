using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class AdditionalCorporatePracticeProfileDto
    {
        [JsonProperty("additionalCorp2PracticeName")]
        public string? AdditionalCorpPracticeName { get; set; }

        [JsonProperty("additionalCorp2RenderingNPI")]
        public string? AdditionalCorpRenderingNPI { get; set; }

        [JsonProperty("additionalCorp2TaxIdNumber")]
        public string? AdditionalCorpTaxIdNumber { get; set; }

        [JsonProperty("additionalCorp2PSpecSubSpec")]
        public string? AdditionalCorpPSpecSubSpec { get; set; }

        [JsonProperty("additionalCorp2aTaxIdName")]
        public string? AdditionalCorpTaxIdName { get; set; }

        [JsonProperty("additionalCorp2PracticePhys")]
        public string? AdditionalCorpPracticePhys { get; set; }

        [JsonProperty("additionalCorp2ProvMailAddres")]
        public string? AdditionalCorpProvMailAddres { get; set; }

        [JsonProperty("additionalCorp2ProvPhoneNum")]
        public string? AdditionalCorpProvPhoneNum { get; set; }

        [JsonProperty("additionalCorp2EmployerIdPhys")]
        public string? AdditionalCorpEmployerIdPhys { get; set; }

        [JsonProperty("additionalCorp2EntityType")]
        public string? AdditionalCorpEntityType { get; set; }

        [JsonProperty("additionalCorp2W9Number")]
        public string? AdditionalCorpW9Number { get; set; }
    }
}
