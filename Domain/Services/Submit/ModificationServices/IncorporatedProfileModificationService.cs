using AutoMapper;
using cred_system_back_end_app.Application.Common.EqualityComparers;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Domain.Services.Submit.DTO;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Domain.Services.Submit.ModificationServices
{
    public class IncorporatedProfileModificationService : EntityModificationServiceBase
    {
        private readonly IMapper _mapper;
        private readonly IProviderCorporationRepository _corporationRepository;
        private readonly CorporationSubSpecialtyComparer _corporationSubSpecialtyComparer;
        private readonly DbContextEntity _dbContextEntity;

        public IncorporatedProfileModificationService(
            DbContextEntity dbContextEntity,
            IMapper mapper,
            IProviderCorporationRepository corporationRepository,
            CorporationSubSpecialtyComparer corporationSubSpecialtyComparer
        ) : base(dbContextEntity, mapper)
        {
            _dbContextEntity = dbContextEntity;
            _mapper = mapper;
            _corporationRepository = corporationRepository;
            _corporationSubSpecialtyComparer = corporationSubSpecialtyComparer;
        }

        public async Task Modify(int providerId, List<CorporationDTO>? corporationDTOs)
        {
            var currentProviderCorporations = await _corporationRepository.GetProviderCorporationsByProviderId(providerId);

            if (corporationDTOs.IsNullOrEmpty())
            {
                await DeleteProviderCorporations(currentProviderCorporations);
            }

            foreach (var corporationDTO in corporationDTOs)
            {
                var corpNpiCertificateDto = corporationDTO.CorporateNpiCertificateFile;
                var corpCertificateDto = corporationDTO.CorporationCertificateFile;
                var corpW9Dto = corporationDTO.W9File;

                var documentLocationCorpNpiCertificate = _dbContextEntity.DocumentLocation
                                             .Where(r => r.ProviderId == providerId
                                             && r.DocumentTypeId == corpNpiCertificateDto.DocumentTypeId
                                             && r.UploadFilename == corpNpiCertificateDto.Name).FirstOrDefault();

                corporationDTO.CorporateNpiCertificateFile.AzureBlobFilename = documentLocationCorpNpiCertificate.AzureBlobFilename;

                var documentLocationcorpCertificate = _dbContextEntity.DocumentLocation
                                             .Where(r => r.ProviderId == providerId
                                             && r.DocumentTypeId == corpCertificateDto.DocumentTypeId
                                             && r.UploadFilename == corpCertificateDto.Name).FirstOrDefault();

                corporationDTO.CorporationCertificateFile.AzureBlobFilename = documentLocationcorpCertificate.AzureBlobFilename;

                if (corpW9Dto != null)
                {
                    var corpW9 = _dbContextEntity.DocumentLocation
                                                 .Where(r => r.ProviderId == providerId
                                                 && r.DocumentTypeId == corpW9Dto.DocumentTypeId
                                                 && r.UploadFilename == corpW9Dto.Name).FirstOrDefault();

                    corporationDTO.W9File.AzureBlobFilename = corpW9.AzureBlobFilename;
                }
            }

            var newProviderCorporations = Application.Common.Mappers.DTOToEntity.Corporation
                .GetProviderCorporationEntities(corporationDTOs, providerId)
                .ToList();


            var publicIdToSubspecialtiesMap = GetPublicIdToSubSpecialtiesMap(corporationDTOs);

            await ModifyList
            (
                currentProviderCorporations,
                newProviderCorporations,
                newProviderCorp => UpdateProviderCorporation(currentProviderCorporations, publicIdToSubspecialtiesMap, newProviderCorp),
                newProviderCorp => InsertProviderCorporation(publicIdToSubspecialtiesMap, newProviderCorp),
                corporationsToDelete => DeleteProviderCorporations(corporationsToDelete)
            );

        }

        #region helpers

        private async Task DeleteProviderCorporations(IEnumerable<ProviderCorporationEntity> providerCorpsToDelete)
        {
            foreach (var oldProviderCorp in providerCorpsToDelete)
            {
                _dbContextEntity.Remove(oldProviderCorp);

                _dbContextEntity.CorporationSubSpecialty
                    .Where(x => x.CorporationEntityId == oldProviderCorp.CorporationId)
                    .ExecuteDelete();

                //_dbContextEntity.CorporationDocument
                //.Where(x => x.CorporationId == oldProviderCorp.CorporationId)
                //.ExecuteDelete();

                _dbContextEntity.Remove(oldProviderCorp.Corporation);
            }
        }

        private async Task InsertProviderCorporation(IDictionary<string, int[]> publicIdToSubspecialtiesMap, ProviderCorporationEntity? newProviderCorp)
        {
            _dbContextEntity.Add(newProviderCorp);

            publicIdToSubspecialtiesMap
                .TryGetValue(newProviderCorp.PublicId, out var subSpecialtyIds);

            if (!subSpecialtyIds.IsNullOrEmpty())
            {
                AddCorporationSubSpecialties(subSpecialtyIds, newProviderCorp.Corporation);
            }
        }

        private async Task UpdateProviderCorporation(List<ProviderCorporationEntity> currentProviderCorporations, IDictionary<string, int[]> publicIdToSubspecialtiesMap, ProviderCorporationEntity? submittedProviderCorp)
        {
            var currentProviderCorp = currentProviderCorporations
                .Single(o => o.PublicId == submittedProviderCorp.PublicId);

            // TODO: AddressIds are re-generated for some reason.
            // This affects auditing, because modification columns are never set.
            // try attaching entity and
            // and setting entityState = modified
            _mapper.Map(submittedProviderCorp, currentProviderCorp);

            publicIdToSubspecialtiesMap
                .TryGetValue(submittedProviderCorp.PublicId, out var subSpecialtyIds);

            if (!subSpecialtyIds.IsNullOrEmpty())
            {
                await UpdateCorporationSubspecialties(subSpecialtyIds, currentProviderCorp.CorporationId);
            }
        }

        private void AddCorporationSubSpecialties(int[] subSpecialties, CorporationEntity corporationEntity)
        {
            var corporationSubSpecialties = Application.Common.Mappers.DTOToEntity.Corporation
                .GetCorporationSubspecialties(subSpecialties, corporationEntity);

            _dbContextEntity.AddRange(corporationSubSpecialties);
        }

        private async Task UpdateCorporationSubspecialties(int[] submittedSpecialtyIds, int corporationId)
        {
            var currentSpecialties = _dbContextEntity.CorporationSubSpecialty
                .Where(c => c.CorporationEntityId == corporationId)
                .ToList();

            var newSpecialties = submittedSpecialtyIds.Select(s => new CorporationSubSpecialtyEntity
            {
                SubSpecialtyListEntityId = s,
                CorporationEntityId = corporationId
            });

            await ModifyRelations(newSpecialties, currentSpecialties, _corporationSubSpecialtyComparer);
        }

        private IDictionary<string, int[]> GetPublicIdToSubSpecialtiesMap(IEnumerable<CorporationDTO> corporationDTOs)
        {
            var dictionary = new Dictionary<string, int[]>();

            foreach (var corpDTO in corporationDTOs)
            {
                dictionary.Add(corpDTO.PublicId, corpDTO.Subspecialty);
            }

            return dictionary;
        }

        #endregion
    }
}
