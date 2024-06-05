using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class AdditionalDirectoryDto
    {
        [JsonProperty("adddirName")]
        public string? AdddirName { get; set; }

        [JsonProperty("adddirAddress")]
        public string? AdddirAddress { get; set; }

        [JsonProperty("adddirEmail")]
        public string? AdddirEmail { get; set; }

        [JsonProperty("adddirMailAddrs")]
        public string? AdddirMailAddrs { get; set; }

        [JsonProperty("adddirPhone")]
        public string? AdddirPhone { get; set; }

        [JsonProperty("adddirFacDisability")]
        public string? AdddirFacDisability { get; set; }

        [JsonProperty("adddirAcceptNewPat")]
        public string? AdddirAcceptNewPat { get; set; }

        [JsonProperty("adddirNPI")]
        public string? AdddirNPI { get; set; }

        [JsonProperty("adddirTransfClose")]
        public string? AdddirTransfClose { get; set; }

        [JsonProperty("adddirOfficeHours")]
        public string? AdddirOfficeHours { get; set; }

        [JsonProperty("adddirNewAffiliat")]
        public string? AdddirNewAffiliat { get; set; }

        [JsonProperty("adddirNewPractice")]
        public string? AdddirNewPractice { get; set; }

        [JsonProperty("adddirPractiveMove")]
        public string? AdddirPractiveMove { get; set; }

        [JsonProperty("adddirPlanAccepted")]
        public string? AdddirPlanAccepted { get; set; }
    }
}
