using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Services.Submit.DTO;

namespace cred_system_back_end_app.Application.Common.Mappers.DTOToEntity
{
    public class Provider
    {
        public static ProviderDetailEntity GetProviderDetailEntity(IndividualPracticeProfileDTO providerDetail, int providerId)
        {
            return new ProviderDetailEntity
            {
                CitizenshipTypeId = providerDetail.CitizenshipTypeId,
                ProviderId = providerId,
                TaxId = providerDetail.TaxId,
                //NpiCertificateNumber = providerDetail.NpiCertificateNumber,
                PRMedicalLicenseNumber = providerDetail.PrMedicalLicenseNumber,
                //NegativePenalCertificateIssuedDate = DateTimeHelper.ParseDate(providerDetail.NegativePenalCertificateIssuedDate),
                //NegativePenalCertificateExpDate = DateTimeHelper.ParseDate(providerDetail.NegativePenalCertificateExpDate),
                SSN = providerDetail.SSN,
                IdType = providerDetail.IdType,
                IdExpDate = DateTimeHelper.ParseDate(providerDetail.IdExpDate)
            };
        }

        public static ProviderAddressEntity[] GetProviderAddressEntities(AddressAndLocationDTO addressAndLocationDTO, int providerId)
        {
            var addressServiceHours = ServiceHoursHelper
                .GetAddressServiceHourEntities(addressAndLocationDTO.ServiceHours);

            return new ProviderAddressEntity[]
            {
                new ProviderAddressEntity {
                    PublicId = addressAndLocationDTO.PublicId + "-physical",
                    ProviderId = providerId,
                    Address = AddressHelper.GetAddressEntityWithServiceHours(addressAndLocationDTO.AddressInfo.Physical, addressServiceHours, AddressTypes.Physical),
                    AdaComplyComment = addressAndLocationDTO.AdaCompliantComment,
                    AddressPrincipalTypeId = addressAndLocationDTO.AddressPrincipalTypeId,
                    IsAcceptingNewPatient = addressAndLocationDTO.IsAcceptingNewPatient,
                    IsComplyWithAda = addressAndLocationDTO.IsComplyWithAda,
                    AddressMedicaidID = addressAndLocationDTO.MedicalId,
                    IsAdaptedToDiabledPatient = addressAndLocationDTO.IsAcceptingNewPatient,
                    IsMovedMoreThan5Miles = addressAndLocationDTO.IsPhysicalAddressSameAsMail,
                },
                new ProviderAddressEntity {
                    PublicId = addressAndLocationDTO.PublicId + "-mail",
                    ProviderId = providerId,
                    Address = AddressHelper.GetAddressEntity(addressAndLocationDTO.AddressInfo.Mail, AddressTypes.Mail),
                    AdaComplyComment = addressAndLocationDTO.AdaCompliantComment,
                    AddressPrincipalTypeId = addressAndLocationDTO.AddressPrincipalTypeId,
                    IsAcceptingNewPatient = addressAndLocationDTO.IsAcceptingNewPatient,
                    IsComplyWithAda = addressAndLocationDTO.IsComplyWithAda,
                    AddressMedicaidID = addressAndLocationDTO.MedicalId,
                    IsAdaptedToDiabledPatient = addressAndLocationDTO.IsAcceptingNewPatient,
                    IsMovedMoreThan5Miles = addressAndLocationDTO.IsPhysicalAddressSameAsMail,
                }
            };
        }

        public static IEnumerable<ProviderAddressEntity> GetAllProviderAddressEntities(IEnumerable<AddressAndLocationDTO> addressAndLocationDTOs, int providerId)
        {
            return addressAndLocationDTOs.SelectMany(a => GetProviderAddressEntities(a, providerId)).ToList();
        }

        public static IEnumerable<ProviderPlanAcceptEntity> GetProviderPlanAcceptEntities(int[] acceptedPlans, int providerId)
        {
            return acceptedPlans.Select(acceptedPlan =>
            {

                return new ProviderPlanAcceptEntity
                {
                    ProviderId = providerId,
                    PlanAcceptListId = acceptedPlan,
                    IsActive = true
                };
            });
        }
    }
}
