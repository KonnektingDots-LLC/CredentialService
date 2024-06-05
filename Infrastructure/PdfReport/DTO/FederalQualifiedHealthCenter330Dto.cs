using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class FederalQualifiedHealthCenter330Dto
    {
        [JsonProperty("f330GroupName")]
        public string? F330GroupName { get; set; }

        [JsonProperty("f330BillingNPI")]
        public string? F330BillingNPI { get; set; }

        [JsonProperty("f330TaxIdNumber")]
        public string? F330TaxIdNumber { get; set; }

        [JsonProperty("f330MedicaidId")]
        public string? F330MedicaidId { get; set; }

        [JsonProperty("f330RenderingNPI")]
        public string? F330RenderingNPI { get; set; }

        [JsonProperty("f330PhysicalAddress")]
        public string? F330PhysicalAddress { get; set; }

        [JsonProperty("f330MailAddress")]
        public string? F330MailAddress { get; set; }

        [JsonProperty("f330EndorsementDate")]
        public string? F330EndorsementDate { get; set; }

        [JsonProperty("f330ContactPhoneNum")]
        public string? F330ContactPhoneNum { get; set; }

        [JsonProperty("f330EmployerIdNum")]
        public string? F330EmployerIdNum { get; set; }

        [JsonProperty("f330Email")]
        public string? F330Email { get; set; }

        [JsonProperty("f330Specialist")]
        public string? F330Specialist { get; set; }

        [JsonProperty("f330ProvSpecialty")]
        public string? F330ProvSpecialty { get; set; }

        [JsonProperty("f330VITALServHours")]
        public string? F330VITALServHours { get; set; }
    }
}
