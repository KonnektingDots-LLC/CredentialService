using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class AddressServiceHourResponseDto
    {
        public int Id { get; set; }
        [Required]
        public string AddressId { get; set; }
        [Required]
        public string? DayOfWeek { get; set; }
        public TimeOnly? HourFrom { get; set; }
        public TimeOnly? HourTo { get; set; }
        public bool? IsClosed { get; set; }
        public string? Comment { get; set; }
        public string? CreatedBy { get; set; }
        
        public DateTime? CreationDate { get; set; }
        
        public string? ModifiedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public bool IsActive { get; set; }
    }
}
