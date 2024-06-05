using cred_system_back_end_app.Application.UseCase.Submit.DTO;
using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.Common.Mappers.DTOToEntity
{
    public static class Hospital
    {
        public static IEnumerable<HospitalEntity> GetHospitalEntities(SubmitRequestDTO submitData)
        {
            var hospitals = new List<HospitalEntity>();

            var primaryHospitalData = submitData.Content.HospitalAffiliations.Primary;
            
            hospitals.Add(GetHospitalEntity(primaryHospitalData));

            if (submitData.Content.HospitalAffiliations.Secondary != null)
            {
                 var secondaryHospitalData = submitData.Content.HospitalAffiliations.Secondary;
                hospitals.Add(GetHospitalEntity(secondaryHospitalData, isSecondary: true));
            }

            return hospitals;
        }        
        
        public static IEnumerable<ProviderHospitalEntity> GetProviderHospitalEntities(SubmitRequestDTO submitData, int providerId)
        {
            var hospitalEntities = GetHospitalEntities(submitData);

            return hospitalEntities.Select(hospital => new ProviderHospitalEntity
            {
                ProviderId = providerId,
                Hospital = hospital
            });
        }

        #region helpers
        public static HospitalEntity GetHospitalEntity(HospitalDTO hospitalDTO, bool isSecondary = false)
        {
            var periodEntity = new PeriodEntity
            {
                PeriodMonthFrom = hospitalDTO.ProviderStartingMonth,
                PeriodYearFrom = hospitalDTO.ProviderStartingYear,
                PeriodMonthTo = hospitalDTO.ProviderEndingMonth,
                PeriodYearTo = hospitalDTO.ProviderEndingYear
            };

            return new HospitalEntity
            {
                HospPriviledgeListId = hospitalDTO.HospitalPrivilegesType,
                HospitalPrivilegesTypeOther = hospitalDTO.HospitalPrivilegesTypeOther,
                HospitalPriviledgePeriod = periodEntity,
                HospitalListId = hospitalDTO.HospitalListId,
                IsSecondary = isSecondary,
                HospitalOther = hospitalDTO.HospitalListOther,
            };
        }
        #endregion
    }
}
