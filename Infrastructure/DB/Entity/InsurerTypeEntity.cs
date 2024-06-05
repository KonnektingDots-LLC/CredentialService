using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class InsurerTypeEntity
    {
        public int Id { get; set; }
        [Required]
        public int InsurerTypeListId { get; set; }
        [Required]
        public int InsurerCompanyId { get; set; }
        [Required]
        public string? InsurerName { get; set; }
        [Required]
        public string? InsurerEmail { get; set; }
        [Required]
        public string? CreatedBy { get; set; }
        
        public DateTime? CreationDate { get; set; }
        
        public string? ModifiedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public bool IsActive { get; set; }
    }
}
