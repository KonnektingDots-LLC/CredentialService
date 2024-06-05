using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Application.CRUD.Hospital.DTO
{
    public class CreateHospitalDto
    {
        public int Id { get; set; }
        [Required]
        public int HospPriviledgeListId { get; set; }
        [Required]
        public DateTime? HospPriviledgeMonthFrom { get; set; }
        [Required]
        public DateTime? HospPriviledgeYearFrom { get; set; }
        [Required]
        public DateTime? HospPriviledgeMonthTo { get; set; }
        [Required]
        public DateTime? HospPriviledgeYearTo { get; set; }
        [Required]
        public string? HospitalName { get; set; }
        public int ProviderId { get; set; }
        [Required]
        public int HospContactId { get; set; }
        [Required]
        public int HospMailingAddressId { get; set; }
        [Required]
        public int HospPhysicalAddressId { get; set; }
        [Required]
        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
