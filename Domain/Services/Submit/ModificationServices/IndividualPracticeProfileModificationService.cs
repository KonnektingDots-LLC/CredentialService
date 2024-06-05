using AutoMapper;
using cred_system_back_end_app.Application.Common.EqualityComparers;
using cred_system_back_end_app.Domain.Services.Submit.DTO;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;

namespace cred_system_back_end_app.Domain.Services.Submit.ModificationServices
{
    public class IndividualPracticeProfileModificationService : EntityModificationServiceBase
    {
        private readonly DbContextEntity _dbContextEntity;

        public IndividualPracticeProfileModificationService
        (
            DbContextEntity dbContextEntity,
            IMapper mapper
        )
            : base(dbContextEntity, mapper)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task Modify(IndividualPracticeProfileDTO individualPracticeProfileDTO, int providerId)
        {
            var newProviderDetail = Application.Common.Mappers.DTOToEntity.Provider
                .GetProviderDetailEntity(individualPracticeProfileDTO, providerId);

            var oldProviderDetail = _dbContextEntity.ProviderDetail
                .Single(p => p.ProviderId == providerId);

            await ModifyEntity(newProviderDetail, oldProviderDetail);

            var providerPlanComparer = new ProviderPlanComparer();

            var newProviderPlans = Application.Common.Mappers.DTOToEntity.Provider
                .GetProviderPlanAcceptEntities(individualPracticeProfileDTO.PlanAccept, providerId);

            var oldProviderPlans = _dbContextEntity.ProviderPlanAccept
                .Where(p => p.ProviderId == providerId)
                .ToList();

            await ModifyRelations(newProviderPlans, oldProviderPlans, providerPlanComparer);
        }
    }
}
