using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Domain.Entities
{
    public class MGContactEntity
    {
        public int Id { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public string? MGContactFullName { get; set; }
        public string? MGContactPhoneNumber { get; set; }
        public string? MGContactEmail { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
