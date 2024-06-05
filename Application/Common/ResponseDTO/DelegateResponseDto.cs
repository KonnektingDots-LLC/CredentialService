using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class DelegateResponseDto
    {
        public int Id { get; set; }
        [Required]
        public int DelegateTypeId { get; set; }
        [Required]
        public int ProviderDelegateId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string? DelegateFullName { get; set; }
        [Required]
        public string? DelegateEmail { get; set; }
        [Required]
        public string? CreatedBy { get; set; }
        
        public DateTime? CreationDate { get; set; }
        
        public string? ModifiedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public bool IsActive { get; set; }
    }
}
