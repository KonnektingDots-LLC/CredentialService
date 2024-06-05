using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace cred_system_back_end_app.Application.CRUD.Provider.DTO
{
    public class CreatedProviderResponseDto
    {
        [JsonPropertyName("providerId")]
        public int Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }

    }
}
