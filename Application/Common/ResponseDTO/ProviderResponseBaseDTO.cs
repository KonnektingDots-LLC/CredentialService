using System.Text.Json.Serialization;

namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class ProviderResponseBaseDTO
    {
        public int ProviderId { get; set; }

        public string Name { get; set; }
        public string MiddleName { get; set; }

        public string  LastName { get; set; }
        public string  SurName { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string RenderingNPI { get; set; }

        public string BillingNPI { get; set; }

        //[JsonIgnore]
        //public int PrioritySorting { get; set; }
        [JsonIgnore]
        public int CredFormId { get; set; }
        public string StatusName { get; set; }
    }
}
