using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class EdMedicalSchoolEntity
    {
        public int Id { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public string? EdMedicalSchoolName { get; set; }
        
        public string? EdMSAddressName { get; set; }
        
        public string? EdMSAddressLine1 { get; set; }
        
        public string? EdMSAddressLine2 { get; set; }
        
        public string? EdMSAddressCity { get; set; }
        
        public string? EdMSAddressState { get; set; }
        
        public string? EdMSAddressZipCode { get; set; }
        
        public string? EdMSAddressCounty { get; set; }
        
        public DateTime? EdMSAttendanceFromMonth { get; set; }
        
        public DateTime? EdMSAttendanceFromYear { get; set; }
        
        public DateTime? EdMSAttendanceToMonth { get; set; }
        
        public DateTime? EdMSAttendanceToYear { get; set; }
        
        public DateTime? EdMSGraduationDate { get; set; }
        
        public string? EdMSSpecialty { get; set; }
        
        public DateTime? EdMSSpecialtyCompletionDate { get; set; }
        
        public string? EdMSSpecialtyDegreeReceived { get; set; }
        
        public bool? EdMSDiplomaEvidence { get; set; }
        
        public string? CreatedBy { get; set; }
        
        public DateTime? CreationDate { get; set; }
        
        public string? ModifiedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public bool IsActive { get; set; }
    }
}
