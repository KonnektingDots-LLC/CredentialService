using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class HospitalAffiliationsDto
    {
        [JsonProperty("Hospital1Affiliations")]
        public Hospital1AffiliationsDto? Hospital1Affiliations { get; set; }

        [JsonProperty("Hospital2Affiliations")]
        public Hospital2AffiliationsDto? Hospital2Affiliations { get; set; }
    }
}
