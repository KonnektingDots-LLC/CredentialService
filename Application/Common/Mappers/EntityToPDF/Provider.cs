using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;

namespace cred_system_back_end_app.Application.Common.Mappers.EntityToPDF
{
    public static class Provider
    {
        public static IndPrimaryPracticeProfile1Dto GetIndPrimaryPracticeProfileDTO(
            ProviderEntity providerData,
            string subSpecialtyName)
        {

            var physicalAddress = AddressHelper.GetAllAddressesByType(providerData.Address, AddressTypes.Physical);
            var postalAddress = AddressHelper.GetAllAddressesByType(providerData.Address, AddressTypes.Mail);

            return new IndPrimaryPracticeProfile1Dto
            {
                ProviderFirstName = providerData.FirstName,
                ProviderLastName = providerData.LastName,
                ProviderMiddleName = providerData.MiddleName,
                ProvDateOfBirth = providerData.BirthDate.ToShortDateString(),
                ProvGender = providerData.Gender,
                ProvIRenderingNpi = providerData.RenderingNPI,
                ProvSSN = providerData.ProviderDetail.SSN,
                ProvIndivTaxId = providerData.ProviderDetail.TaxId,
                ProvIndivMedLic = providerData.ProviderDetail.PRMedicalLicenseNumber,

                // TODO: Un provider puede tener varios subspecialties.
                ProvIndSubSpecialty = subSpecialtyName,
                ProvIPhysicalAddress = physicalAddress?.FirstOrDefault()?.GetFormattedAddressString(),
                ProvIndMailAddress = postalAddress?.FirstOrDefault()?.GetFormattedAddressString(),
                ProvIndivEmail = providerData.Email,
                ProvIndivOfficePhone = providerData.PhoneNumber,
            };
        }
    }
}
