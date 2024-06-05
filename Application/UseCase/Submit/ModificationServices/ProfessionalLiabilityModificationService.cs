using AutoMapper;
using cred_system_back_end_app.Application.UseCase.Submit.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;

namespace cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices
{
    public class ProfessionalLiabilityModificationService : EntityModificationServiceBase
    {
        private readonly DbContextEntity _dbContextEntity;

        public ProfessionalLiabilityModificationService
        (
            DbContextEntity dbContextEntity,
            IMapper mapper
        )
            : base(dbContextEntity, mapper)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task Modify(ProfessionalLiabilityDTO professionalLiabilityDTO, int providerId)
        {
            var newProfessionalLiabilty = Common.Mappers.DTOToEntity.InsuranceHelper
                .GetProfessionalLiabilityEntities(professionalLiabilityDTO, providerId);

            var oldProfessionalLiablity = _dbContextEntity.ProfessionalLiability
                .Where(o => o.ProviderId == providerId).FirstOrDefault();

            await ModifyEntity(newProfessionalLiabilty, oldProfessionalLiablity);
        }
    }
}
