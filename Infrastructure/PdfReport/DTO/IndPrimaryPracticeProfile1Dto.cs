using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class IndPrimaryPracticeProfile1Dto
    {
        [JsonProperty("providerFirstName")]
        public string? ProviderFirstName { get; set; }

        [JsonProperty("providerLastName")]
        public string? ProviderLastName { get; set; }

        [JsonProperty("providerMiddleName")]
        public string? ProviderMiddleName { get; set; }

        [JsonProperty("provDateOfBirth")]
        public string? ProvDateOfBirth { get; set; }

        [JsonProperty("provGender")]
        public string? ProvGender { get; set; }

        [JsonProperty("provIRenderingNpi")]
        public string? ProvIRenderingNpi { get; set; }

        [JsonProperty("provSSN")]
        public string? ProvSSN { get; set; }

        [JsonProperty("provIndivTaxId")]
        public string? ProvIndivTaxId { get; set; }

        [JsonProperty("provIndivMedLic")]
        public string? ProvIndivMedLic { get; set; }

        [JsonProperty("provIndSubSpecialty")]
        public string? ProvIndSubSpecialty { get; set; }

        [JsonProperty("provIPhysicalAddress")]
        public string? ProvIPhysicalAddress { get; set; }

        [JsonProperty("provIndMailAddress")]
        public string? ProvIndMailAddress { get; set; }

        [JsonProperty("provIndivOfficePhone")]
        public string? ProvIndivOfficePhone { get; set; }

        [JsonProperty("provIndivEmail")]
        public string? ProvIndivEmail { get; set; }
    }
}
