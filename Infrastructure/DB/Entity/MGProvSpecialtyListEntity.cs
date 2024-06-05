using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class MGProvSpecialtyListEntity
    {
        public int Id { get; set; }
        [Required]
        public int MedicalGroupId { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public int ProviderSpecialtyId { get; set; }
        [Required]
        public string? CreatedBy { get; set; }
        
        public DateTime? CreationDate { get; set; }
        
        public string? ModifiedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public bool IsActive { get; set; }

    }
}
