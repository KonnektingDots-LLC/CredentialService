using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Domain.Entities
{
    public class MultipleNPIEntity
    {
        public int Id { get; set; }
        /* BEGIN EF Relation */
        public int ProviderId { get; set; }
        public ProviderEntity Provider { get; set; }
        /* END EF Relation */

        [MaxLength(14)]
        public string NPI { get; set; }
        [MaxLength(50)]
        public string CorporateName { get; set; }

        [Required, MaxLength(255)]
        public string CreatedBy { get; set; } = "lleon";
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [AllowNull, MaxLength(255)]
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
