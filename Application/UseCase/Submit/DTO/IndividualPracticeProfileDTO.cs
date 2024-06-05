using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Application.UseCase.Submit.DTO
{
    public class IndividualPracticeProfileDTO
    {
        public int CitizenshipTypeId { get; set; }

        [MaxLength(9), MinLength(9)]
        public string TaxId { get; set; }

        [MaxLength(15), MinLength(5)]
        public string PrMedicalLicenseNumber { get; set; }
        public FileBaseDTO NpiCertificateFile { get; set; }
        public string NpiCertificateNumber { get; set; }
        //public FileBaseDTO NegativePenalCertificateFile { get; set; }
        //public string NegativePenalCertificateIssuedDate { get; set; }
        //public string NegativePenalCertificateExpDate { get; set; }
        public FileBaseDTO CurriculumVitaeFile { get; set; }
        public string SSN { get; set; }
        public int IdType { get; set; }
        public string IdExpDate { get; set; }
        public FileBaseDTO IdFile { get; set; }
        public FileBaseDTO? ForeignPassportFile { get; set; }
        public int[] PlanAccept { get; set; }
    }
}
