using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class EdInternshipInstitutionEntity
    {
        public int Id { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public string? EdInternshipInstitutionName { get; set; }
        
        public string? EdIIAddressName { get; set; }
        
        public string? EdIIAddressLine1 { get; set; }
        
        public string? EdIIAddressLine2 { get; set; }
        
        public string? EdIIAddressCity { get; set; }
        
        public string? EdIIAddressState { get; set; }
        
        public string? EdIIAddressZipCode { get; set; }
        
        public string? EdIIAddressCounty { get; set; }
        
        public DateTime? EdIIAttendanceFromMonth { get; set; }
        
        public DateTime? EdIIAttendanceFromYear { get; set; }
        
        public DateTime? EdIIAttendanceToMonth { get; set; }
        
        public DateTime? EdIIAttendanceToYear { get; set; }
                
        public string? EdIIProgramType { get; set; }
                
        public bool? EdIIEvidence { get; set; }
        
        public string? CreatedBy { get; set; }
        
        public DateTime? CreationDate { get; set; }
        
        public string? ModifiedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public bool IsActive { get; set; }
    }
}
