using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Application.CRUD.SubSpecialty.DTO
{
    public class CreateSubSpecialtyDto
    {
        public int Id { get; set; }
        [Required]
        public int ProviderSubSpecialtyListId { get; set; }
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
