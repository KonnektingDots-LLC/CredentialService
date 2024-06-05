using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Domain.Services.Submit.DTO.EducationDTOs;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;

namespace cred_system_back_end_app.Domain.Services.Submit.ModificationServices.EducationModificationServices
{
    public class ResidencyModificationService : EducationInfoModificationServiceBase<ResidencyDTO>
    {
        public ResidencyModificationService(DbContextEntity dbContextEntity, IMapper mapper, IDocumentLocationRepository documentLocationRepository)
            : base(dbContextEntity, mapper, EducationTypes.Residency, documentLocationRepository)
        {

        }

        protected override IEnumerable<ProviderEducationInfoEntity> GetNewEducationEntities(IEnumerable<ResidencyDTO> residencyDTOs, int providerId)
        {
            return Application.Common.Mappers.DTOToEntity.Education
                .GetProviderResidencyEntities(residencyDTOs, providerId);
        }
    }
}
