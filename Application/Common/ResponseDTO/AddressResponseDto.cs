using cred_system_back_end_app.Infrastructure.DB.Entity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class AddressResponseDto
    {
        public int Id { get; set; }
        [Required]
        //public int AddressTypeListId { get; set; }
        //[Required]
        //public int AddressContacttId { get; set; }
        //[Required]
        //public int ServiceHourId { get; set; }
        //[Required]
        //public int ProviderId { get; set; }
        //[Required]
        //public int EmployerId { get; set; }
        //[Required]
        //public int CorporationId { get; set; }
        //[Required]
        //public int MedicalGroupId { get; set; }
        //[Required]
        //public int HospitalId { get; set; }
        //[Required]
        //public int AddressMedicaidId { get; set; }
        //[Required]
        public string? AddressName { get; set; }
        
        public string? AddressLine1 { get; set; }
        
        public string? AddressLine2 { get; set; }
        
        public string? AddressCity { get; set; }
        
        public string? AddressState { get; set; }
        
        public string? AddressZipCode { get; set; }
        
        public string? AddressCounty { get; set; }
        //
        //public bool? IsClosed { get; set; }
        //public bool? IsPrincipal { get; set; }
        //public bool? MovedMoredThan5Miles { get; set; }
        //public bool? AdaptedToDisabledPatients { get; set; }
        //public bool? AcceptNewPatients { get; set; }
        //public bool? ADARequirement { get; set; }
        //public string? ADAComment { get; set; }
        //public string? CreatedBy { get; set; }
        //
        //public DateTime? CreationDate { get; set; }
        //
        //public string? ModifiedBy { get; set; }
        //
        //public DateTime? ModifiedDate { get; set; }
        //
        //public bool IsActive { get; set; }
    }
}
