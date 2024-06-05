using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Services.Submit.DTO;

namespace cred_system_back_end_app.Application.Common.Mappers.DTOToEntity
{
    public class Specialties
    {
        public static IEnumerable<ProviderSpecialtyEntity> GetProviderSpecialtyEntities
            (
                IEnumerable<SpecialtyDTO> specialties,
                IEnumerable<DocumentLocationEntity> documents,
                int providerId
            )
        {
            return specialties.Select(specialty => GetProviderSpecialtyEntity(documents, specialty, providerId));
        }

        public static IEnumerable<ProviderSubSpecialtyEntity> GetProviderSubSpecialtyEntities(IEnumerable<SpecialtyDTO> subspecialties, IEnumerable<DocumentLocationEntity> documents, int providerId)
        {
            return subspecialties.Select(specialty => GetProviderSubSpecialtyEntity(documents, specialty, providerId));
        }

        #region helpers

        private static ProviderSpecialtyEntity GetProviderSpecialtyEntity(IEnumerable<DocumentLocationEntity> documents, SpecialtyDTO specialtyDTO, int providerId)
        {
            string azureBlobFilename = GetAzureBlobFileName(documents, specialtyDTO, providerId);

            return new ProviderSpecialtyEntity
            {
                ProviderId = providerId,
                SpecialtyListId = specialtyDTO.Id,
                AzureBlobFileName = azureBlobFilename,
            };
        }

        private static string GetAzureBlobFileName(IEnumerable<DocumentLocationEntity> documents, SpecialtyDTO specialtyDTO, int providerId)
        {
            return documents
                .Where(d => d.UploadFilename == specialtyDTO.EvidenceFile.Name
                && d.DocumentTypeId == specialtyDTO.EvidenceFile.DocumentTypeId
                && d.ProviderId == providerId)
                .FirstOrDefault()
                ?.AzureBlobFilename;
        }

        private static ProviderSubSpecialtyEntity GetProviderSubSpecialtyEntity(IEnumerable<DocumentLocationEntity> documents, SpecialtyDTO subSpecialtyDTO, int providerId)
        {
            string azureBlobFilename = GetAzureBlobFileName(documents, subSpecialtyDTO, providerId);

            return new ProviderSubSpecialtyEntity
            {
                ProviderId = providerId,
                SubSpecialtyListId = subSpecialtyDTO.Id,
                DocumentLocationId = azureBlobFilename
            };
        }

        #endregion
    }
}
