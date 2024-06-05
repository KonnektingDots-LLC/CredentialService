using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Application.DTO.Responses
{
    public class CorporationResponseDto
    {
        public int Id { get; set; }
        [Required]
        public int CorpProvSpecialtyTypeListId { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public int CorporationSpecialtyListId { get; set; }
        [Required]
        public int CorporationSubSpecialtyListId { get; set; }
        [Required]
        public string? CorporatePracticeName { get; set; }
        [Required]
        public DateTime? IncorporationEffectiveDate { get; set; }
        [Required]
        public string? CorporateNPI { get; set; }
        [Required]
        public string? CorporateRenderingNPI { get; set; }
        [Required]
        public string? CorporateBillingNPI { get; set; }

        public string? CorporateTaxId_EIN { get; set; }
        [Required]
        public string? EmployerId { get; set; }
        [Required]
        public string? EntityType { get; set; }

        public int PhyscialAddressId { get; set; }
        [Required]
        public int MailingAddressId { get; set; }
        [Required]
        public bool? CorporationCertificateEvidence { get; set; }

        public bool? NPICertificateEvidence { get; set; }

        public bool? EIN_W9Evidence { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
