using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class CorporatePracticeProfile2Dto
    {
        [JsonProperty("corpPracticeName")]
        public string? CorpPracticeName { get; set; }

        [JsonProperty("corpIncEffectiveDate")]
        public string? CorpIncEffectiveDate { get; set; }

        [JsonProperty("corpNPINumber")]
        public string? CorpNPINumber { get; set; }

        [JsonProperty("corpRenderingNPI")]
        public string? CorpRenderingNPI { get; set; }

        [JsonProperty("corpTaxIdNumber")]
        public string? CorpTaxIdNumber { get; set; }

        [JsonProperty("corpProvSpecialty")]
        public string? CorpProvSpecialty { get; set; }

        [JsonProperty("corpProvSubSpecialty")]
        public string? CorpProvSubSpecialty { get; set; }

        [JsonProperty("corpPracticePhys")]
        public string? CorpPracticePhys { get; set; }

        [JsonProperty("corpProvMailAddress")]
        public string? CorpProvMailAddress { get; set; }

        [JsonProperty("corpProvMedicaidId")]
        public string? CorpProvMedicaidId { get; set; }

        [JsonProperty("corpProvPhoneNumber")]
        public string? CorpProvPhoneNumber { get; set; }

        [JsonProperty("corpEmployerIdPhys")]
        public string? CorpEmployerIdPhys { get; set; }

        [JsonProperty("corpEntityType")]
        public string? CorpEntityType { get; set; }

        [JsonProperty("corpW9Number")]
        public string? CorpW9Number { get; set; }
    }
}
