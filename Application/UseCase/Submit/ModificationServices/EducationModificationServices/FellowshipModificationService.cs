using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.CRUD.Document;
using cred_system_back_end_app.Application.UseCase.Submit.DTO.EducationDTOs;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices.EducationResubmitServices
{
    public class FellowshipModificationService : EducationInfoModificationServiceBase<FellowshipDTO>
    {
        public FellowshipModificationService(DbContextEntity dbContextEntity, IMapper mapper, DocumentCase documentCase) 
            : base(dbContextEntity, mapper, EducationTypes.Fellowship, documentCase){ }

        protected override IEnumerable<ProviderEducationInfoEntity> GetNewEducationEntities(IEnumerable<FellowshipDTO> fellowshipDTOs, int providerId) 
        {
            return Common.Mappers.DTOToEntity.Education
                .GetProviderFellowshipEntities(fellowshipDTOs, providerId);
        }
    }
}