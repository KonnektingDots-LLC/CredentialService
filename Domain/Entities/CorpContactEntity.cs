using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Domain.Entities
{
    public class CorpContactEntity
    {
        public int Id { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public string? CorpContactFullName { get; set; }
        public string? CorpContactPhoneNumber { get; set; }
        public string? CorpContactEmail { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
