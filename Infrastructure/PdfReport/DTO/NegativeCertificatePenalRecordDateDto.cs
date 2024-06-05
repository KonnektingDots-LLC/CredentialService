using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class NegativeCertificatePenalRecordDateDto
    {
        [JsonProperty("negCertPenalRecDate")]
        public string? NegCertPenalRecDate { get; set; }
    }
}
