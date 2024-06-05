using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class FormSectionsDto
    {
        [JsonProperty("IndPrimaryPracticeProfile1")]
        [Required]
        public IndPrimaryPracticeProfile1Dto IndPrimaryPracticeProfile1 { get; set; }

        [JsonProperty("CorporatePracticeProfile2")]
        [Required]
        public CorporatePracticeProfile2Dto CorporatePracticeProfile2 { get; set; }

        [JsonProperty("AdditionalCorporatePracticeProfile")]
        [Required]
        public List<AdditionalCorporatePracticeProfileDto> AdditionalCorporatePracticeProfile { get; set; }

        [JsonProperty("PrimaryCarePhysicianPCP")]
        [Required]
        public PrimaryCarePhysicianPCPDto PrimaryCarePhysicianPCP { get; set; }

        [JsonProperty("FederalQualifiedHealthCenter330")]
        [Required]
        public FederalQualifiedHealthCenter330Dto FederalQualifiedHealthCenter330 { get; set; }

        [JsonProperty("HospitalAffiliations")]
        [Required]
        public HospitalAffiliationsDto HospitalAffiliations { get; set; }

        [JsonProperty("EducationAndTraining")]
        [Required]
        public EducationAndTrainingDto EducationAndTraining { get; set; }

        [JsonProperty("LicenseAndCertification")]
        [Required]
        public LicenseAndCertificationDto LicenseAndCertification { get; set; }

        [JsonProperty("NegativeCertificatePenalRecordDate")]
        [Required]
        public NegativeCertificatePenalRecordDateDto NegativeCertificatePenalRecordDate { get; set; }

        [JsonProperty("Malpractice")]
        [Required]
        public MalpracticeDto Malpractice { get; set; }

        [JsonProperty("ProfessionalLiability")]
        [Required]
        public ProfessionalLiabilityDto ProfessionalLiability { get; set; }


        [JsonProperty("AdditionalDirectory")]
        [Required]
        public AdditionalDirectoryDto AdditionalDirectory { get; set; }
    }
}
