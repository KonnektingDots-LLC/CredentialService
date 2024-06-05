using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class AddressTypeListResponseDto
    {
        public int Id { get; set; }
        [Required]
        public string? AddressTypeListCode { get; set; }
        public string? AddressTypeListName { get; set; }
        public string? CreatedBy { get; set; }
        
        public DateTime? CreationDate { get; set; }
        
        public string? ModifiedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public bool IsActive { get; set; }
    }
}
