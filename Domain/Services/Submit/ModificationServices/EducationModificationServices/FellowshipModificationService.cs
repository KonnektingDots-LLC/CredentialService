using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Domain.Services.Submit.DTO.EducationDTOs;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;

namespace cred_system_back_end_app.Domain.Services.Submit.ModificationServices.EducationModificationServices
{
    public class FellowshipModificationService : EducationInfoModificationServiceBase<FellowshipDTO>
    {
        public FellowshipModificationService(DbContextEntity dbContextEntity, IMapper mapper, IDocumentLocationRepository documentLocationRepository)
            : base(dbContextEntity, mapper, EducationTypes.Fellowship, documentLocationRepository) { }

        protected override IEnumerable<ProviderEducationInfoEntity> GetNewEducationEntities(IEnumerable<FellowshipDTO> fellowshipDTOs, int providerId)
        {
            return Application.Common.Mappers.DTOToEntity.Education
                .GetProviderFellowshipEntities(fellowshipDTOs, providerId);
        }
    }
}