using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.CRUD.Document;
using cred_system_back_end_app.Application.UseCase.Submit.DTO.EducationDTOs;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices.EducationResubmitServices
{
    public abstract class EducationInfoModificationServiceBase<T> : EntityModificationServiceBase
    {
        private readonly DbContextEntity _dbContextEntity;
        private readonly IMapper _mapper;
        protected readonly int _educationType;
        private readonly DocumentCase _documentCase;

        public EducationInfoModificationServiceBase
        (
            DbContextEntity dbContextEntity,
            IMapper mapper,
            int educationType,
            DocumentCase documentCase
        ) : base(dbContextEntity, mapper)
        {
            _dbContextEntity = dbContextEntity;
            _mapper = mapper;
            _educationType = educationType;
            _documentCase = documentCase;
        }

        protected abstract IEnumerable<ProviderEducationInfoEntity> GetNewEducationEntities(IEnumerable<T> educationDTOs, int providerId);

        protected virtual IEnumerable<ProviderEducationInfoEntity> GetOldEducationEntities(int providerId) 
        {
            return _dbContextEntity
                  .ProviderEducationInfo
                  .Where
                  (
                    p => p.ProviderId == providerId &&
                    p.EducationInfo.EducationTypeId == _educationType
                  )
                  .Include(pe => pe.EducationInfo)
                  .ThenInclude(ei => ei.EducationPeriod)
                  .Include(pe => pe.EducationInfo)
                  .ThenInclude(ei => ei.Address)
                  .Include(pe => pe.EducationInfo)
                  .ThenInclude (ei => ei.EducationInfoDocument)
                  .ToList();
        }

        public async Task Modify(int providerId, IEnumerable<T> educationDTOs)
        {
            foreach (var educationDTO in educationDTOs)
            {
                if (educationDTO is InternshipDTO internshipDTO)
                {
                    var documentLocationIntership = _documentCase.GetDocumentLocationEntityByProviderIdDocTypeFilename(
                                                                    providerId,
                                                                    internshipDTO.EvidenceFile.DocumentTypeId,
                                                                    internshipDTO.EvidenceFile.Name);

                    internshipDTO.EvidenceFile.AzureBlobFilename = documentLocationIntership?.AzureBlobFilename;
                }

                if (educationDTO is ResidencyDTO residencyDTO)
                {
                    var documentLocationIntership = _documentCase.GetDocumentLocationEntityByProviderIdDocTypeFilename(
                                                                    providerId,
                                                                    residencyDTO.EvidenceFile.DocumentTypeId,
                                                                    residencyDTO.EvidenceFile.Name);

                    residencyDTO.EvidenceFile.AzureBlobFilename = documentLocationIntership?.AzureBlobFilename;
                }

                if (educationDTO is FellowshipDTO fellowshipDTO)
                {
                    var documentLocationIntership = _documentCase.GetDocumentLocationEntityByProviderIdDocTypeFilename(
                                                                    providerId,
                                                                    fellowshipDTO.EvidenceFile.DocumentTypeId,
                                                                    fellowshipDTO.EvidenceFile.Name);

                    fellowshipDTO.EvidenceFile.AzureBlobFilename = documentLocationIntership?.AzureBlobFilename;
                }

            }

            var newEducationEntities = GetNewEducationEntities(educationDTOs, providerId);

            var oldEducationEntities = GetOldEducationEntities(providerId);

            await ModifyList
                (
                    oldEducationEntities, 
                    newEducationEntities, 
                    newEducationEntity => UpdateEducation(newEducationEntity, oldEducationEntities), 
                    newInternshipEntity => AddListMember(newInternshipEntity),
                    oldEducationEntities => DeleteEntities(oldEducationEntities)
                );
        }

        #region helper
        public async Task DeleteEntities(IEnumerable<ProviderEducationInfoEntity> oldEducationEntities)
        {
            foreach (var oldEducationEntity in oldEducationEntities) 
            { 
                _dbContextEntity.Remove(oldEducationEntity.EducationInfo.Address);
                _dbContextEntity.Remove(oldEducationEntity.EducationInfo.EducationPeriod);               
                _dbContextEntity.Remove(oldEducationEntity);
                _dbContextEntity.Remove(oldEducationEntity.EducationInfo);               
            }
        }

        protected async Task UpdateEducation(ProviderEducationInfoEntity newEntity, IEnumerable<ProviderEducationInfoEntity> currentEntities)
        {
            var currentEntity = currentEntities
                .Single(c => c.PublicId == newEntity.PublicId);

            _mapper.Map(newEntity.EducationInfo.EducationPeriod, currentEntity.EducationInfo.EducationPeriod);
            _mapper.Map(newEntity, currentEntity);
        }

        #endregion
    }
}