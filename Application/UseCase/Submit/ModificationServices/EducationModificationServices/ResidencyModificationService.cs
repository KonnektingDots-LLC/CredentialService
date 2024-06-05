using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.CRUD.Document;
using cred_system_back_end_app.Application.UseCase.Submit.DTO.EducationDTOs;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices.EducationResubmitServices
{
    public class ResidencyModificationService : EducationInfoModificationServiceBase<ResidencyDTO>
    {
        public ResidencyModificationService(DbContextEntity dbContextEntity, IMapper mapper, DocumentCase documentCase)
            : base(dbContextEntity, mapper, EducationTypes.Residency, documentCase) 
        { 
        
        }

        protected override IEnumerable<ProviderEducationInfoEntity> GetNewEducationEntities(IEnumerable<ResidencyDTO> residencyDTOs, int providerId)
        {
            return Common.Mappers.DTOToEntity.Education
                .GetProviderResidencyEntities(residencyDTOs, providerId);
        }
    }
}
