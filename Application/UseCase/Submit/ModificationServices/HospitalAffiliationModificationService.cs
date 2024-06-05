using AutoMapper;
using cred_system_back_end_app.Application.Common.Mappers.DTOToEntity;
using cred_system_back_end_app.Application.UseCase.Submit.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices
{
    public class HospitalAffiliationModificationService : EntityModificationServiceBase
    {
        private readonly DbContextEntity _dbContextEntity;

        public HospitalAffiliationModificationService
        (
            DbContextEntity dbContextEntity,
            IMapper mapper
        )
            : base(dbContextEntity, mapper)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task Modify(HospitalAffiliationsDTO hospitalAffiliationsDTO, int providerId)
        {
            var oldProviderHospitals = _dbContextEntity.ProviderHospital
                .Where(p => p.ProviderId == providerId)
                .Include(p => p.Hospital)
                .ThenInclude(h => h.HospitalPriviledgePeriod)
                .ToList();

            var secondaryExists = oldProviderHospitals.Where(p => p.Hospital.IsSecondary == true).Any();

            var oldHospitals = oldProviderHospitals.Select(h => h.Hospital);

            await ModifyHospital(hospitalAffiliationsDTO, oldHospitals);

            if (hospitalAffiliationsDTO.Secondary != null)
            {
                
                if (secondaryExists)
                {
                    await ModifyHospital(hospitalAffiliationsDTO, oldHospitals, isSecondary: true);
                }
                else
                {
                    var hospitals = GetProviderHospitalEntities(hospitalAffiliationsDTO.Secondary, providerId);

                    _dbContextEntity.AddRange(hospitals);
                }
            }
            else
            {

                if (secondaryExists)
                {
                    _dbContextEntity.Remove(oldProviderHospitals.Single(h => h.Hospital.IsSecondary == true));
                }
                
            }
        }
        public static IEnumerable<HospitalEntity> GetHospitalEntities(HospitalDTO submitData)
        {
            var hospitals = new List<HospitalEntity>();

                
            var secondaryHospitalData = submitData;
            hospitals.Add(GetHospitalEntity(secondaryHospitalData, isSecondary: true));
           

            return hospitals;
        }
        public static IEnumerable<ProviderHospitalEntity> GetProviderHospitalEntities(HospitalDTO submitData, int providerId)
        {
            var hospitalEntities = GetHospitalEntities(submitData);

            return hospitalEntities.Select(hospital => new ProviderHospitalEntity
            {
                ProviderId = providerId,
                Hospital = hospital
            });
        }

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

        private async Task ModifyHospital(
            HospitalAffiliationsDTO hospitalAffiliationsDTO, 
            IEnumerable<HospitalEntity> oldHospitals,
            bool isSecondary = false)
        {
            var oldHospital = oldHospitals.Single(h => h.IsSecondary == isSecondary);

            var newHospitalDTO = isSecondary ? hospitalAffiliationsDTO.Secondary : hospitalAffiliationsDTO.Primary;

            var newHospital = Common.Mappers.DTOToEntity.Hospital
                .GetHospitalEntity(newHospitalDTO, isSecondary);

           await ModifyEntity(newHospital.HospitalPriviledgePeriod, oldHospital.HospitalPriviledgePeriod);
           await ModifyEntity(newHospital, oldHospital);
           await ModifyEntity(newHospital, oldHospital);   
        }

        private void AddHospitalEntities(SubmitRequestDTO submitData, int providerId)
        {
            // Hospitals Affiliations

            if (submitData.Content.Setup.HospitalAffiliationsApplies)
            {
                var providerHospitalEntities = Hospital.GetProviderHospitalEntities(submitData, providerId);
                _dbContextEntity.AddRange(providerHospitalEntities);
            }
        }
    }
}
