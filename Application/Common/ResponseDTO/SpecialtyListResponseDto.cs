using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class SpecialtyListResponseDto
    {
        public int Id { get; set; }
        [Required]
        public string? ProviderSpecialtyCode { get; set; }
        public string? ProviderSpecialtyName { get; set; }
        public string? CreatedBy { get; set; }
        
        public DateTime? CreationDate { get; set; }
        
        public string? ModifiedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public bool IsActive { get; set; }
    }
}
