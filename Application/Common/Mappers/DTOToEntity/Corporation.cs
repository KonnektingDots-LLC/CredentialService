using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Services.Submit.DTO;

namespace cred_system_back_end_app.Application.Common.Mappers.DTOToEntity
{
    public static class Corporation
    {
        public static IEnumerable<CorporationEntity> GetCorporationEntities(SubmitRequestDTO submitDto)
        {
            var providerId = submitDto.Content.Setup.ProviderId;
            var corporations = submitDto.Content.IncorporatedPracticeProfile;

            return corporations.Select(corporation => GetCorporationEntity(corporation));
        }

        public static ProviderCorporationEntity GetProviderCorporationEntity(
            CorporationEntity corporationEntity,
            int providerId,
            string publicId
        )
        {
            return new ProviderCorporationEntity
            {
                PublicId = publicId,
                ProviderId = providerId,
                Corporation = corporationEntity,
            };
        }

        public static ProviderCorporationEntity GetProviderCorporationEntity(CorporationDTO corporationDTO, int providerId)
        {
            return new ProviderCorporationEntity
            {
                PublicId = corporationDTO.PublicId,
                ProviderId = providerId,
                Corporation = GetCorporationEntity(corporationDTO),
            };
        }

        public static IEnumerable<ProviderCorporationEntity> GetProviderCorporationEntities(IEnumerable<CorporationDTO> corporationDTOs, int providerId)
        {
            return corporationDTOs.Select(c => GetProviderCorporationEntity(c, providerId));
        }

        public static CorporationAddressEntity GetCorporationAddressEntity(CorporationEntity corporationEntity)
        {
            return new CorporationAddressEntity
            {
                Corporation = corporationEntity,
                CreatedBy = "waldarondo",
                CreationDate = DateTime.Now,
            };
        }

        public static CorporationAddressEntity GetCorporationAddressEntity(CorporationDTO corporationDTO)
        {
            return new CorporationAddressEntity
            {
                Corporation = GetCorporationEntity(corporationDTO),
                Address = AddressHelper.GetAddressEntities(corporationDTO.AddressInfo).ToList().First(),
                CreatedBy = "waldarondo",
                CreationDate = DateTime.Now,
            };
        }

        public static CorporationEntity GetCorporationEntity(CorporationDTO corporationData)
        {
            var addresses = AddressHelper.GetAddressEntities(corporationData.AddressInfo).ToList();

            addresses.Add(AddressHelper.GetAddressEntity(corporationData.EmployerIdAddressInfo, AddressTypes.EmployerIdPhysical));

            var newCorporationEntity = new CorporationEntity()
            {
                CorpTaxIdTypeId = corporationData.CorporateTaxType,
                ParticipateMedicaid = corporationData.ParticipateMedicaid,
                MedicaidIdLocation = corporationData.MedicaidIdLocation,
                CorpTaxIdNumber = corporationData.CorporateTaxNumber,
                EntityTypeId = corporationData.EntityTypeId,
                CorporatePracticeName = corporationData.CorporatePracticeName,
                ContactPhoneNumber = corporationData.CorporatePhoneNumber,
                IncorporationEffectiveDate = DateTimeHelper.ParseDate(corporationData.IncorporationEffectiveDate),
                BillingNPI = corporationData.CorporationNpiNumber,
                RenderingNPI = corporationData.RenderingNpiNumber,
                Address = addresses
            };

            List<CorporationDocumentEntity> corporationDocumentEntities = new List<CorporationDocumentEntity>();


            CorporationDocumentEntity corporationDocumentEntity1 = new CorporationDocumentEntity()
            {

                DocumentLocationId = corporationData.CorporateNpiCertificateFile.AzureBlobFilename,
                Corporation = newCorporationEntity
            };

            CorporationDocumentEntity corporationDocumentEntity2 = new CorporationDocumentEntity()
            {

                DocumentLocationId = corporationData.CorporationCertificateFile.AzureBlobFilename,
                Corporation = newCorporationEntity
            };

            corporationDocumentEntities.Add(corporationDocumentEntity1);
            corporationDocumentEntities.Add(corporationDocumentEntity2);

            if (corporationData.W9File != null)
            {
                CorporationDocumentEntity corporationDocumentEntity3 = new CorporationDocumentEntity()
                {

                    DocumentLocationId = corporationData.W9File.AzureBlobFilename,
                    Corporation = newCorporationEntity
                };
                corporationDocumentEntities.Add(corporationDocumentEntity3);
            }

            newCorporationEntity.CorporationDocument = corporationDocumentEntities;

            return newCorporationEntity;

        }

        public static (CorporationEntity corp, string publicId) GetCorporationEntityPairs(CorporationDTO corporationData)
        {
            var addresses = AddressHelper.GetAddressEntities(corporationData.AddressInfo).ToList();

            addresses.Add(AddressHelper.GetAddressEntity(corporationData.EmployerIdAddressInfo, AddressTypes.EmployerIdPhysical));

            return (GetCorporationEntity(corporationData), corporationData.PublicId);
        }

        public static IEnumerable<CorporationSubSpecialtyEntity> GetCorporationSubspecialties(int[] subSpecialties, CorporationEntity corporationEntity)
        {
            return subSpecialties.Select(subspecialty => new CorporationSubSpecialtyEntity
            {
                SubSpecialtyListEntityId = subspecialty,
                Corporation = corporationEntity
            });
        }
    }
}
