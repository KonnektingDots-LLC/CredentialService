using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class PrimaryCarePhysicianPCPDto
    {
        [JsonProperty("pcpGroupName")]
        public string? PcpGroupName { get; set; }

        [JsonProperty("pcpBillingNPI")]
        public string? PcpBillingNPI { get; set; }

        [JsonProperty("pcpTaxIdNumber")]
        public string? PcpTaxIdNumber { get; set; }

        [JsonProperty("pcpMedicaidId")]
        public string? PcpMedicaidId { get; set; }

        [JsonProperty("pcpRenderingNPI")]
        public string? PcpRenderingNPI { get; set; }

        [JsonProperty("pcpPhysicalAddress")]
        public string? PcpPhysicalAddress { get; set; }

        [JsonProperty("pcpMailAddress")]
        public string? PcpMailAddress { get; set; }

        [JsonProperty("pcpEndorsementDate")]
        public string? PcpEndorsementDate { get; set; }

        [JsonProperty("pcpContactPhoneNum")]
        public string? PcpContactPhoneNum { get; set; }

        [JsonProperty("pcpEmployerIdNum")]
        public string? PcpEmployerIdNum { get; set; }

        [JsonProperty("pcpEmail")]
        public string? PcpEmail { get; set; }

        [JsonProperty("pcpSpecialist")]
        public string? PcpSpecialist { get; set; }

        [JsonProperty("pcpProvSpecialty")]
        public string? PcpProvSpecialty { get; set; }

        [JsonProperty("pcpVITALServHours")]
        public string? PcpVITALServHours { get; set; }
    }
}
