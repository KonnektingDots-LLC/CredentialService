using AutoMapper;
using cred_system_back_end_app.Domain.Services.Submit.DTO;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;

namespace cred_system_back_end_app.Domain.Services.Submit.ModificationServices
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
            var newProfessionalLiabilty = Application.Common.Mappers.DTOToEntity.InsuranceHelper
                .GetProfessionalLiabilityEntities(professionalLiabilityDTO, providerId);

            var oldProfessionalLiablity = _dbContextEntity.ProfessionalLiability
                .Where(o => o.ProviderId == providerId).FirstOrDefault();

            await ModifyEntity(newProfessionalLiabilty, oldProfessionalLiablity);
        }
    }
}
