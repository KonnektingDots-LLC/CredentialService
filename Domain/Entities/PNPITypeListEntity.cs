using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Domain.Entities
{
    public class PNPITypeListEntity
    {
        public int Id { get; set; }
        public string? NPITypeNme { get; set; }
        [Required, MaxLength(12)]
        public string? CreatedBy { get; set; }
        [Required, DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }
        [AllowNull, MaxLength(12)]
        public string? ModifiedBy { get; set; }
        [AllowNull, DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
