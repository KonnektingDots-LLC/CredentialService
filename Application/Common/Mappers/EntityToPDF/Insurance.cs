using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;

namespace cred_system_back_end_app.Application.Common.Mappers.EntityToPDF
{
    public class Insurance
    {
        public static MalpracticeDto GetMalpracticeDto(MalpracticeEntity malpracticeData, string OIGCaseNumbers)
        {
            return new MalpracticeDto
            {
                MalpCarrierName = GetInsuranceCarrierName(malpracticeData),
                MalpEffectiveFrom = malpracticeData.InsurancePolicyEffectiveDate?.ToString(DateFormats.IIPCA_DATE_FROMAT),
                MalpEffectiveTo = malpracticeData.InsurancePolicyExpirationDate?.ToString(DateFormats.IIPCA_DATE_FROMAT),
                MalpPolicyNum = malpracticeData.PolicyNumber.ToString(),
                MalpCoverageAmt = new MalpCoverageAmountDto
                {
                    AggregateLimit = malpracticeData.CoverageAggregateLimit,
                    PerOccurrence = malpracticeData.CoverageAmountPerOcurrence
                },
                MalpOIGCaseNum = OIGCaseNumbers,
            };
        }

        public static ProfessionalLiabilityDto GetProfessionalLiabilityDto(ProfessionalLiabilityEntity professionalLiabilityData)
        {
            return new ProfessionalLiabilityDto
            {
                ProfLCarrierName = GetInsuranceCarrierName(professionalLiabilityData),
                ProfLPolicyNum = professionalLiabilityData.PolicyNumber.ToString(),
                ProfLCoverageAmt = new ProfCoverageAmountDto
                {
                    AggregateLimit = professionalLiabilityData.CoverageAggregateLimit,
                    PerOccurrence = professionalLiabilityData.CoverageAmountPerOccurence
                },
                ProfLEffectiveFrom = professionalLiabilityData.InsurancePolicyEffectiveDate?.ToString(DateFormats.IIPCA_DATE_FROMAT),
                ProfLEffectiveTo = professionalLiabilityData.InsurancePolicyExpirationDate?.ToString(DateFormats.IIPCA_DATE_FROMAT)
            };
        }

        public static string GetInsuranceCarrierName(MalpracticeEntity malpracticeEntity)
        {
            return malpracticeEntity.MalpracticeCarrier.Name.ToUpper() == "OTHER" ?
                   malpracticeEntity.MalpracticeCarrierOther :
                   malpracticeEntity.MalpracticeCarrier.Name;
        }


        public static string GetInsuranceCarrierName(ProfessionalLiabilityEntity professionalLiabilityEntity)
        {
            return professionalLiabilityEntity.ProfessionalLiabilityCarrier.Name.ToUpper() == "OTHER" ?
                   professionalLiabilityEntity.ProfessionalLiabilityCarrierOther :
                   professionalLiabilityEntity.ProfessionalLiabilityCarrier.Name;
        }
    }
}
