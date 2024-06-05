using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Domain.Entities
{
    public class HospContactEntity
    {
        public int Id { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public string? HospContactFullName { get; set; }
        public string? HospContactPhoneNumber { get; set; }
        public string? HospContactEmail { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
