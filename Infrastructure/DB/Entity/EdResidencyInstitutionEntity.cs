using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class EdResidencyInstitutionEntity
    {
        public int Id { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public string? EdResidencyInstitutionName { get; set; }
        
        public string? EdRIAddressName { get; set; }
        
        public string? EdRIAddressLine1 { get; set; }
        
        public string? EdRIAddressLine2 { get; set; }
        
        public string? EdRIAddressCity { get; set; }
        
        public string? EdRIAddressState { get; set; }
        
        public string? EdRIAddressZipCode { get; set; }
        
        public string? EdRIAddressCounty { get; set; }
        
        public DateTime? EdRIAttendanceFromMonth { get; set; }
        
        public DateTime? EdRIAttendanceFromYear { get; set; }
        
        public DateTime? EdRIAttendanceToMonth { get; set; }
        
        public DateTime? EdRIAttendanceToYear { get; set; }
        
        public string? EdRIProgramType { get; set; }
        
        public DateTime? EdRICompletionDate { get; set; }
        
        public bool? EdRIEvidence { get; set; }
        
        public string? CreatedBy { get; set; }
        
        public DateTime? CreationDate { get; set; }
        
        public string? ModifiedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public bool IsActive { get; set; }
    }
}
