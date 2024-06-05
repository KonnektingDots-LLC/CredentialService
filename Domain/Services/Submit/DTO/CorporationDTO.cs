using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Domain.Services.Submit.DTO
{
    public class CorporationDTO
    {
        public string PublicId { get; set; }
        public int CorporateTaxType { get; set; }

        public bool ParticipateMedicaid { get; set; }

        /// <summary>
        /// If ParticipateMedicaid == true =>
        /// Primary or Specialty care.
        /// </summary>
        public int? SpecialtyType { get; set; }

        /// <summary>
        /// If ParticipateMedicaid == true =>
        /// Specify subspecialty
        /// </summary>
        public int[]? Subspecialty { get; set; }

        /// <summary>
        /// If ParticipateMedicaid == true =>
        /// </summary>
        public string? MedicaidIdLocation { get; set; }

        public AddressInfoDTO AddressInfo { get; set; }

        public string CorporatePracticeName { get; set; }

        public string IncorporationEffectiveDate { get; set; }

        public string CorporationNpiNumber { get; set; }

        public string RenderingNpiNumber { get; set; }

        [MaxLength(9), MinLength(9)]
        public string CorporateTaxNumber { get; set; }

        public string CorporatePhoneNumber { get; set; }

        public AddressDTO EmployerIdAddressInfo { get; set; }

        public int EntityTypeId { get; set; }

        public FileBaseDTO CorporationCertificateFile { get; set; }

        public FileBaseDTO CorporateNpiCertificateFile { get; set; }

        public FileBaseDTO? W9File { get; set; }
    }

    public class EmployerIdAddress
    {
        public AddressDTO Physical { get; set; }
    }
}
