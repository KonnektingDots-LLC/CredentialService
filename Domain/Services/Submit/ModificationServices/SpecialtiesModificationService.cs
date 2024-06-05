using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.EqualityComparers;
using cred_system_back_end_app.Application.Common.Mappers.DTOToEntity;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Services.Submit.DTO;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Domain.Services.Submit.ModificationServices
{
    public class SpecialtiesModificationService : EntityModificationServiceBase
    {
        private readonly DbContextEntity _dbContextEntity;
        private readonly ProviderSpecialtyComparer _providerSpecialtyComparer;
        private readonly ProviderSubspecialtyComparer _providerSubspecialtyComparer;

        public SpecialtiesModificationService
        (
            DbContextEntity dbContextEntity,
            IMapper mapper,
            ProviderSpecialtyComparer providerSpecialtyComparer,
            ProviderSubspecialtyComparer providerSubspecialtyComparer
        )
            : base(dbContextEntity, mapper)
        {
            _dbContextEntity = dbContextEntity;
            _providerSpecialtyComparer = providerSpecialtyComparer;
            _providerSubspecialtyComparer = providerSubspecialtyComparer;
        }

        public async Task Modify(SpecialtiesAndSubspecialtiesDTO specialtiesAndSubspecialtiesDTO, int providerId)
        {
            var providerSpecialtyDocuments = GetDocumentsByType(providerId, DocumentTypes.Specialty);

            if (providerSpecialtyDocuments.IsNullOrEmpty())
            {

            }

            var newProviderSpecialties = Specialties
                .GetProviderSpecialtyEntities(specialtiesAndSubspecialtiesDTO.Specialties, providerSpecialtyDocuments, providerId);

            var oldProviderSpecialties = await _dbContextEntity.ProviderSpecialty
                .Where(p => p.ProviderId == providerId)
                .ToListAsync();

            await ModifyRelations(newProviderSpecialties, oldProviderSpecialties, _providerSpecialtyComparer);

            var providerSubSpecialtyDocuments = GetDocumentsByType(providerId, DocumentTypes.SubSpecialty);

            if (!providerSubSpecialtyDocuments.IsNullOrEmpty())
            {
            }

            var newProviderSubspecialties = Specialties
                .GetProviderSubSpecialtyEntities(specialtiesAndSubspecialtiesDTO.Subspecialties, providerSubSpecialtyDocuments, providerId);

            var oldProviderSubspecialties = _dbContextEntity.ProviderSubSpecialty
                .Where(p => p.ProviderId == providerId)
                .ToList();

            await ModifyRelations(newProviderSubspecialties, oldProviderSubspecialties, _providerSubspecialtyComparer);
        }

        #region helpers
        private IEnumerable<DocumentLocationEntity> GetDocumentsByType(int providerId, int documentType)
        {
            return _dbContextEntity.DocumentLocation
                 .Where(dl => dl.ProviderId == providerId)
                 .Where(dl => dl.DocumentTypeId == documentType)
                 .ToList();
        }
        #endregion
    }
}
