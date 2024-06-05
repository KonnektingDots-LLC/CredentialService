using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ProviderTypeEntity
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }


        [Required, MaxLength(255)]
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }

        [AllowNull, MaxLength(255)]
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; } = true;

        public bool IsExpired { get; set; }
        public DateTime? ExpiredDate { get; set; }

        /* BEGIN EF Relation */
        public List<ProviderEntity> Provider { get; set; }
    }
}
