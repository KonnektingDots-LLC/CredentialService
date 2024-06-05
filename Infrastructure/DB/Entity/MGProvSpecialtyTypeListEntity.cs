using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class MGProvSpecialtyTypeListEntity
    {
        public int Id { get; set; }
        [Required]
        public string? MGProvSpecialtyTypeCode { get; set; }
        public string? MGProvSpecialtyTypeName { get; set; }
        public string? CreatedBy { get; set; }
        
        public DateTime? CreationDate { get; set; }
        
        public string? ModifiedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public bool IsActive { get; set; }
    }
}
