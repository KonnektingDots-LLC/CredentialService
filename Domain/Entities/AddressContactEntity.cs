using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Domain.Entities
{
    public class AddressContactEntity
    {
        public int Id { get; set; }
        [Required]
        public string? AddressContactFullName { get; set; }
        public string? AddressContactPhoneNumber { get; set; }
        public string? AddressContactEmail { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
