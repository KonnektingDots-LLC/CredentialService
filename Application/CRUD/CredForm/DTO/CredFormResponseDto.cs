using System.Text.Json.Serialization;

namespace cred_system_back_end_app.Application.CRUD.CredForm.DTO
{
    public class CredFormResponseDto
    {
        [JsonPropertyName("credFormId")]
        public int Id { get; set; }
        public string Email { get; set; }
        public int Version { get; set; }
        public string State { get; set; }
    }
}
