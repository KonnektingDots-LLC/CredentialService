using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Application.UseCase.Submit.DTO
{
    public class InsuranceDTO
    {
        public MalpracticeDTO Malpractice { get; set; }
        public ProfessionalLiabilityDTO? ProfessionalLiability { get; set; }
        public FileBaseOptionalDto? ActionExplanationFormFile { get; set; }
      
        public FileBaseDTO? Link2File { get; set; }
        public FileBaseDTO? Link3File { get; set; }
        public FileBaseDTO? Link4File { get; set; }
        public FileBaseDTO? Link5File { get; set; }
    }

    public class ProfessionalLiabilityDTO
    {
        public int ProfessionalLiabilityCarrierId { get; set; }
        public string? ProfessionalLiabilityCarrierOther { get; set; }
        public string InsurancePolicyEffectiveDate { get; set; }
        public string InsurancePolicyExpDate { get; set; }
        public string PolicyNumber { get; set; }
        public string CoverageAmountPerOcurrence { get; set; }
        public string CoverageAggregateLimit { get; set; }
        public FileBaseDTO CertificateCoverageFile { get; set; }
    }

    public class MalpracticeDTO
    {
        public int MalpracticeCarrierId { get; set; }
        public string? MalpracticeCarrierOther { get; set; }        
        public string InsurancePolicyEffectiveDate { get; set; }
        public string InsurancePolicyExpDate { get; set; }
        public string PolicyNumber { get; set; }
        [MaxLength(25), MinLength(1)]
        public string CoverageAmountPerOcurrence { get; set; }
        [MaxLength(25), MinLength(1)]
        public string CoverageAggregateLimit { get; set; }
        public FileBaseDTO CertificateCoverageFile { get; set; }

        //TODO: Crear tabla de OIGCases
        public string[] OigCaseNumber { get; set; }
    }
}
