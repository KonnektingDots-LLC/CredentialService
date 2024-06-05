using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Application.CRUD.Specialty.DTO
{
    public class CreateSpecialtyDto
    {
        public int Id { get; set; }
        [Required]
        public int ProviderSpecialtyListId { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
