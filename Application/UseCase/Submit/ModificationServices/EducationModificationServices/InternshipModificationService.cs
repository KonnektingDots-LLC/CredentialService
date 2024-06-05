using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.CRUD.Document;
using cred_system_back_end_app.Application.UseCase.Submit.DTO.EducationDTOs;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices.EducationResubmitServices
{
    public class InternshipModificationService : EducationInfoModificationServiceBase<InternshipDTO>
    {
        public InternshipModificationService(DbContextEntity dbContextEntity, IMapper mapper, DocumentCase documentCase) 
            : base(dbContextEntity, mapper, EducationTypes.Internship, documentCase)
        {

        }

        protected override IEnumerable<ProviderEducationInfoEntity> GetNewEducationEntities(IEnumerable<InternshipDTO> internshipDTOs, int providerId) 
        {
            
            return Common.Mappers.DTOToEntity.Education
                .GetProviderInternshipEntities(internshipDTOs, providerId);
        }
    }
}