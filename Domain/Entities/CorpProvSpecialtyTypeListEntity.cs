using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Domain.Entities
{
    public class CorpProvSpecialtyTypeListEntity
    {
        public int Id { get; set; }
        [Required]
        public string? CorpProvSpecialtyTypeCode { get; set; }
        public string? CorpProvSpecialtyTypeName { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
