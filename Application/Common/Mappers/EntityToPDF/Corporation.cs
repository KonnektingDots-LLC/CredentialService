using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;

namespace cred_system_back_end_app.Application.Common.Mappers.EntityToPDF
{
    public class Corporation
    {
        public static CorporatePracticeProfile2Dto GetCorporatePracticeProfile2DTO(CorporationEntity corporationEntity)
        {
            var (physicalAddress, postalAddress) = AddressHelper.GetPhysicalAndPostalAddresses(corporationEntity.Address);

            var employerIdPhysical = AddressHelper.GetAllAddressesByType(corporationEntity.Address, AddressTypes.EmployerIdPhysical);

            var subSpecialty = corporationEntity?.SubSpecialty?.FirstOrDefault()?.Name;

            return new CorporatePracticeProfile2Dto
            {
                CorpPracticeName = corporationEntity.CorporatePracticeName,
                CorpIncEffectiveDate = corporationEntity.IncorporationEffectiveDate.ToString(DateFormats.IIPCA_DATE_FROMAT),
                CorpNPINumber = corporationEntity.BillingNPI,
                CorpRenderingNPI = corporationEntity.RenderingNPI,
                CorpTaxIdNumber = corporationEntity.CorpTaxIdNumber,
                CorpProvSpecialty = corporationEntity.SpecialtyType == 1 ? "Primary Care" : "Specialty Care",
                CorpProvSubSpecialty = subSpecialty,
                CorpPracticePhys = physicalAddress?.FirstOrDefault()?.GetFormattedAddressString(),
                CorpProvMailAddress = postalAddress?.FirstOrDefault()?.GetFormattedAddressString(),
                CorpProvMedicaidId = corporationEntity.MedicaidIdLocation,
                CorpProvPhoneNumber = corporationEntity.ContactPhoneNumber,
                CorpEmployerIdPhys = employerIdPhysical?.FirstOrDefault()?.GetFormattedAddressString(),
                CorpEntityType = corporationEntity.EntityType.Name,
            };
        }

        public static AdditionalCorporatePracticeProfileDto GetAdditionalCorporatePracticeProfileDto(CorporationEntity corporationEntity)
        {
            var (physicalAddress, postalAddress) = AddressHelper.GetPhysicalAndPostalAddresses(corporationEntity.Address);

            var subSpecialty = corporationEntity?.SubSpecialty?.FirstOrDefault()?.Name;

            var employerIdPhysical = AddressHelper.GetAllAddressesByType(corporationEntity.Address, AddressTypes.EmployerIdPhysical);

            return new AdditionalCorporatePracticeProfileDto
            {

                AdditionalCorpPracticeName = corporationEntity.CorporatePracticeName,
                //AdditionalCorpIncEffectiveDate = corporationEntity.IncorporationEffectiveDate.ToString(),
                //AdditionalCorpNPINumber = corporationEntity.BillingNPI,
                AdditionalCorpRenderingNPI = corporationEntity.RenderingNPI,
                AdditionalCorpTaxIdNumber = corporationEntity.CorpTaxIdNumber,
                //AdditionalCorpProvSpecialty = corporationEntity.SpecialtyType == 1 ? "Primary Care" : "Specialty Care",
                AdditionalCorpPSpecSubSpec = subSpecialty,
                AdditionalCorpPracticePhys = physicalAddress?.FirstOrDefault()?.GetFormattedAddressString(),
                AdditionalCorpProvMailAddres = postalAddress?.FirstOrDefault()?.GetFormattedAddressString(),
                //AdditionalCorpProvMedicaidId = corporationEntity.MedicaidIdLocation,
                AdditionalCorpProvPhoneNum = corporationEntity.ContactPhoneNumber,
                AdditionalCorpEmployerIdPhys = employerIdPhysical?.FirstOrDefault()?.GetFormattedAddressString(),
                AdditionalCorpEntityType = corporationEntity.EntityType.Name,
            };
        }
    }
}
