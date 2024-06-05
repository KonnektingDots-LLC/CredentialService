using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Services.Submit.DTO;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Application.Common.Helpers
{
    public static class AddressHelper
    {
        public static AddressEntity GetAddressEntity(AddressDTO addressDTO, ICollection<AddressServiceHourEntity> serviceHours, int addressTypeId)
        {
            var addressEntity = GetAddressEntity(addressDTO, addressTypeId);

            if (serviceHours != null)
            {
                addressEntity.AddressServiceHours = serviceHours;
            }

            return addressEntity;
        }

        public static AddressEntity GetAddressEntity(AddressDTO addressDTO, int addressTypeId)
        {
            var addressEntity = new AddressEntity
            {
                Name = addressDTO.Name,
                AddressTypeId = addressTypeId,
                Address1 = addressDTO.Address1,
                Address2 = addressDTO?.Address2,
                AddressCountryId = addressDTO.AddressCountryId,
                City = addressDTO.City,
                IsClosed = false,
                IsActive = true,
                CreatedBy = "",
                CreationDate = DateTime.Now,
            };

            addressEntity = GetAddressEntityWithPostalCodes(addressEntity, addressDTO);

            return addressEntity;
        }

        public static AddressEntity GetAddressEntityWithServiceHours(AddressDTO addressDTO, ICollection<AddressServiceHourEntity> serviceHours, int addressTypeId)
        {
            var addressEntity = new AddressEntity
            {
                Name = addressDTO.Name,
                AddressTypeId = addressTypeId,
                Address1 = addressDTO.Address1,
                Address2 = addressDTO?.Address2,
                AddressCountryId = addressDTO.AddressCountryId,
                City = addressDTO.City,
                AddressServiceHours = serviceHours,
                IsClosed = false,
                IsActive = true
            };

            addressEntity = GetAddressEntityWithPostalCodes(addressEntity, addressDTO);

            return addressEntity;
        }

        public static AddressEntity[] GetAddressEntities(AddressInfoDTO addressInfoDTO, ICollection<AddressServiceHourEntity> serviceHours)
        {
            return new AddressEntity[]
            {
                GetAddressEntity(addressInfoDTO.Physical, serviceHours, AddressTypes.Physical),
                GetAddressEntity(addressInfoDTO.Mail, AddressTypes.Mail),
            };
        }

        public static AddressEntity[] GetAddressEntities(AddressAndLocationDTO addressAndLocationDTO)
        {
            ICollection<AddressServiceHourEntity> serviceHours = null;

            if (!addressAndLocationDTO.ServiceHours.IsNullOrEmpty())
            {
                serviceHours = ServiceHoursHelper.GetAddressServiceHourEntities(addressAndLocationDTO.ServiceHours);
            }

            return new AddressEntity[]
            {
                GetAddressEntity(addressAndLocationDTO.AddressInfo.Physical, serviceHours, AddressTypes.Physical),
                GetAddressEntity(addressAndLocationDTO.AddressInfo.Mail, AddressTypes.Mail),
            };
        }

        public static AddressEntity[] GetAddressEntities(AddressInfoDTO addressInfoDTO)
        {
            return new AddressEntity[]
            {
                GetAddressEntity(addressInfoDTO.Physical, AddressTypes.Physical),
                GetAddressEntity(addressInfoDTO.Mail, AddressTypes.Mail),
            };
        }

        public static IEnumerable<AddressEntity> GetAllAddressesByType(this ICollection<AddressEntity> addresses, int addressType)
        {
            return addresses.Where(a => a.AddressTypeId == addressType);
        }

        public static (IEnumerable<AddressEntity>, IEnumerable<AddressEntity>) GetPhysicalAndPostalAddresses(ICollection<AddressEntity> addresses)
        {
            var physical = GetAllAddressesByType(addresses, AddressTypes.Physical);
            var postal = GetAllAddressesByType(addresses, AddressTypes.Mail);

            return (physical, postal);
        }

        public static string GetFormattedAddressString(this AddressEntity address)
        {
            if (address.AddressCountryId == CountryCodes.PR || address.AddressCountryId == CountryCodes.USA)
            {
                return $"{address.Address1} {address.City}, {address.AddressState?.Name} {address.AddressCountry} {address.ZipCode}{(address.ZipCodeExtension.IsNullOrEmpty() ? "" : "-" + address.ZipCodeExtension)}";
            }

            return $"{address.Address1} {address.City}, {address.StateOther}, {address.AddressCountry} {address.InternationalCode}";
        }

        private static AddressEntity GetAddressEntityWithPostalCodes(AddressEntity addressEntity, AddressDTO addressDTO)
        {
            if (addressEntity.AddressCountryId == CountryCodes.PR || addressEntity.AddressCountryId == CountryCodes.USA)
            {
                if (addressDTO.ZipCode == null)
                {
                    throw new AggregateException("Zipcode was not provided.");
                }

                addressEntity.ZipCode = addressDTO.ZipCode;
                addressEntity.ZipCodeExtension = addressDTO.ZipCodeExtension;
                addressEntity.AddressStateId = addressDTO.StateId;

                return addressEntity;
            }

            if (addressDTO.InternationalCode == null)
            {
                throw new AggregateException("This country requires a Postal Code.");
            }

            addressEntity.InternationalCode = addressDTO.InternationalCode;
            addressEntity.AddressStateId = StateCodes.Other;
            addressEntity.StateOther = addressDTO.StateOther;

            return addressEntity;
        }

        public static string GetFormattedCityStateZipCodeCombination(this AddressEntity address)
        {
            return $"{address.City}, {address.AddressState?.Name}, {address.ZipCode}";
        }
    }
}
