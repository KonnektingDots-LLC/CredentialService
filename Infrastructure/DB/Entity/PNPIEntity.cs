using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class PNPIEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(12)]
        public string? NPI { get; set; }
        [Required, MaxLength(12)]
        public string? CreatedBy { get; set; }
        [Required, DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }
        [AllowNull, MaxLength(12)]
        public string? ModifiedBy { get; set; }
        [AllowNull, DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; } = true;
        //[ForeignKey("PNPITypeId")]
        public int PNPITypeId { get; set; }
        //public PNPITypeEntity? PNPITypeEntity { get; set; }
        //[ForeignKey("ProviderId")]       
        public int ProviderId { get; set; }
        //public ProviderEntity? ProviderEntity { get; set; }
    }
}
