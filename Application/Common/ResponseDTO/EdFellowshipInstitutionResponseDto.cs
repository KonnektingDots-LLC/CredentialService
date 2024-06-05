using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class EdFellowshipInstitutionResponseDto
    {
        public int Id { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public string? EdFellowshipInstitutionName { get; set; }
        
        public string? EdFIAddressName { get; set; }
        
        public string? EdFIAddressLine1 { get; set; }
        
        public string? EdFIAddressLine2 { get; set; }
        
        public string? EdFIAddressCity { get; set; }
        
        public string? EdFIAddressState { get; set; }
        
        public string? EdFIAddressZipCode { get; set; }
        
        public string? EdFIAddressCounty { get; set; }
        
        public DateTime? EdFIAttendanceFromMonth { get; set; }
        
        public DateTime? EdFIAttendanceFromYear { get; set; }
        
        public DateTime? EdFIAttendanceToMonth { get; set; }
        
        public DateTime? EdFIAttendanceToYear { get; set; }
        
        public string? EdFIProgramType { get; set; }
        
        public DateTime? EdFICompletionDate { get; set; }
        
        public bool? EdFIEvidence { get; set; }
        
        public string? CreatedBy { get; set; }
        
        public DateTime? CreationDate { get; set; }
        
        public string? ModifiedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public bool IsActive { get; set; }
    }
}
