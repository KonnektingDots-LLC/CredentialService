using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Services.Submit.DTO;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Application.Common.Mappers.DTOToEntity
{
    public class MedicalGroup
    {
        public static MedicalGroupEntity GetMedicalGroupEntity(MedicalGroupDTO medicalGroupDTO, int medicalGroupType)
        {
            ICollection<AddressServiceHourEntity> serviceHours = null;

            if (!medicalGroupDTO.ServiceHours.IsNullOrEmpty())
            {
                serviceHours = ServiceHoursHelper.GetAddressServiceHourEntities(medicalGroupDTO.ServiceHours);
            }

            return new MedicalGroupEntity
            {
                MedicalGroupTypeId = medicalGroupType,
                CareTypeId = medicalGroupDTO.PcpOrSpecialistId,
                SpecifyPrimaryCareId = medicalGroupDTO?.SpecifyPrimaryCareId,
                TypeOfSpecialistId = medicalGroupDTO?.TypeOfSpecialistId,
                Name = medicalGroupDTO.PmgName,
                MedicaidId = medicalGroupDTO.MedicaidIdLocation,
                NPI = medicalGroupDTO.NpiGroupNumber,
                BillingNPI = medicalGroupDTO.BillingNpiNumber,
                TaxId = medicalGroupDTO.TaxIdGroup,
                EndorsementLetterDate = medicalGroupDTO.EndorsementLetterDate.IsNullOrEmpty() ?
                    null : DateTimeHelper.ParseDate(medicalGroupDTO.EndorsementLetterDate),
                EmployerId_EIN = medicalGroupDTO.EmployerId,
                ContactPhone = medicalGroupDTO.ContactNumber,
                EmailAddress = medicalGroupDTO.Email,
                Address = AddressHelper.GetAddressEntities(medicalGroupDTO.AddressInfo, serviceHours).ToList(),
                IsActive = true,
            };
        }
    }
}
