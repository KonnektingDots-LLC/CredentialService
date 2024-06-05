using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.UseCase.Submit.DTO;
using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.Common.Mappers.DTOToEntity
{
    public static class InsuranceHelper
    {
        public static MalpracticeEntity GetMalpracticeEntities(SubmitRequestDTO submitDto, IEnumerable<MalpracticeOIGCaseNumbers> oigCaseNumbers)
        {
            var malpracticeData = submitDto.Content.Insurance.Malpractice;

            return new MalpracticeEntity
            {

                ProviderId = submitDto.Content.Setup.ProviderId,
                MalpracticeCarrierId = malpracticeData.MalpracticeCarrierId,
                InsurancePolicyEffectiveDate = DateTimeHelper.ParseDate(malpracticeData.InsurancePolicyEffectiveDate),
                InsurancePolicyExpirationDate = DateTimeHelper.ParseDate(malpracticeData.InsurancePolicyExpDate),
                PolicyNumber = malpracticeData.PolicyNumber,
                CoverageAmountPerOcurrence = malpracticeData.CoverageAmountPerOcurrence,
                CoverageAggregateLimit = malpracticeData.CoverageAggregateLimit,
                MalpracticeCarrierOther = malpracticeData.MalpracticeCarrierOther,
                MalpracticeOIGCaseNumbers = oigCaseNumbers.ToArray(),
            };
        }

        public static MalpracticeEntity GetMalpracticeEntities(MalpracticeDTO malpracticeData, int providerId)
        {
            var oigCaseNumbers = malpracticeData.OigCaseNumber.Select(o => new MalpracticeOIGCaseNumbers
            {
                OIGCaseNumber = o,
            });

            return new MalpracticeEntity
            {
                ProviderId = providerId,
                MalpracticeCarrierId = malpracticeData.MalpracticeCarrierId,
                InsurancePolicyEffectiveDate = DateTimeHelper.ParseDate(malpracticeData.InsurancePolicyEffectiveDate),
                InsurancePolicyExpirationDate = DateTimeHelper.ParseDate(malpracticeData.InsurancePolicyExpDate),
                PolicyNumber = malpracticeData.PolicyNumber,
                CoverageAmountPerOcurrence = malpracticeData.CoverageAmountPerOcurrence,
                CoverageAggregateLimit = malpracticeData.CoverageAggregateLimit,
                MalpracticeCarrierOther = malpracticeData.MalpracticeCarrierOther,
                MalpracticeOIGCaseNumbers = oigCaseNumbers.ToArray(),
            };
        }

        public static ProfessionalLiabilityEntity GetProfessionalLiabilityEntities(ProfessionalLiabilityDTO professionalLiability, int providerId)
        {
            return new ProfessionalLiabilityEntity
            {   ProviderId = providerId,
                // TODO: validar
                ProfessionalLiabilityCarrierId = professionalLiability.ProfessionalLiabilityCarrierId,
                InsurancePolicyEffectiveDate = DateTimeHelper.ParseDate(professionalLiability.InsurancePolicyEffectiveDate),
                InsurancePolicyExpirationDate = DateTimeHelper.ParseDate(professionalLiability.InsurancePolicyExpDate),
                PolicyNumber = professionalLiability.PolicyNumber,
                CoverageAmountPerOccurence = professionalLiability.CoverageAmountPerOcurrence,
                CoverageAggregateLimit = professionalLiability.CoverageAggregateLimit,
                ProfessionalLiabilityCarrierOther = professionalLiability.ProfessionalLiabilityCarrierOther
            };
        }
    }
}
