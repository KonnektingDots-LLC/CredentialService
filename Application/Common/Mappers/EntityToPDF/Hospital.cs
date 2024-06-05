using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;

namespace cred_system_back_end_app.Application.Common.Mappers.EntityToPDF
{
    public class Hospital
    {
        public static HospitalAffiliationsDto GetHospitalAffiliationsDTO(IEnumerable<HospitalEntity> hospitalEntities)
        {
            var primaryHospital = hospitalEntities.Where(h => !h.IsSecondary).First();
            var secondaryHospital = hospitalEntities.Where(h => h.IsSecondary).FirstOrDefault();
            
            var hospitalAffiliationsDTO = new HospitalAffiliationsDto
            {
                Hospital1Affiliations = GetPrimaryHospitalAffiliationDTO(primaryHospital),
            };

            if (secondaryHospital != null)
            {
                hospitalAffiliationsDTO.Hospital2Affiliations = GetSecondaryHospitalAffiliationDTO(secondaryHospital);
            }

            return hospitalAffiliationsDTO;
        }

        public static Hospital1AffiliationsDto GetPrimaryHospitalAffiliationDTO(HospitalEntity hospitalEntity)
        {
            var periodData = hospitalEntity.HospitalPriviledgePeriod;

            return new Hospital1AffiliationsDto
            {
                Hosp1Name = GetHospitalName(hospitalEntity),
                Hosp1PrivType = GetHospitalPriviledgeType(hospitalEntity),
                Hosp1PrivFrom = periodData.GetFormattedStartDate(),
                Hosp1PrivTo = periodData.GetFormattedEndDate()
            };
        }        
        
        public static Hospital2AffiliationsDto GetSecondaryHospitalAffiliationDTO(HospitalEntity hospitalEntity)
        {
            var periodData = hospitalEntity.HospitalPriviledgePeriod;

            return new Hospital2AffiliationsDto
            {
                Hosp2Name = GetHospitalName(hospitalEntity),
                Hosp2PrivType = GetHospitalPriviledgeType(hospitalEntity),
                Hosp2PrivFrom = periodData.GetFormattedStartDate(),
                Hosp2PrivTo = periodData.GetFormattedEndDate()
            };
        }

        #region helpers

        private static string? GetHospitalName(HospitalEntity hospitalEntity) 
        {
            var hospitalList = hospitalEntity.HospitalList;

            return hospitalList.Name.ToUpper() == "other".ToUpper() ?
                    hospitalEntity.HospitalOther : hospitalList.Name;
        }

        private static string? GetHospitalPriviledgeType(HospitalEntity hospitalEntity)
        {
            var hospitalPriviledgeList = hospitalEntity.HospPriviledgeList;

            return hospitalPriviledgeList.Name.ToUpper() == "other".ToUpper() ?
                    hospitalEntity.HospitalPrivilegesTypeOther : hospitalPriviledgeList.Name;
        }

        #endregion
    }
}
