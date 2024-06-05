using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Delegates.Queries.Handlers
{
    public class GetProvidersByDelegateIdHandler : IRequestHandler<GetProvidersByDelegateIdQuery, List<ProviderByDelegateResponseDto>>
    {
        private readonly IProviderDelegatesRepository _providerDelegatesRepository;
        private readonly IDelegateRepository _delegateRepository;
        private readonly ICredFormRepository _credFormRepository;
        private readonly IGenericRepository<CredFormStatusTypeEntity, string> _credFormStatusTypeRepository;

        public GetProvidersByDelegateIdHandler(IProviderDelegatesRepository providerDelegatesRepository, IDelegateRepository delegateRepository,
            ICredFormRepository credFormRepository, IGenericRepository<CredFormStatusTypeEntity, string> credFormStatusTypeRepository)
        {
            _providerDelegatesRepository = providerDelegatesRepository;
            _delegateRepository = delegateRepository;
            _credFormRepository = credFormRepository;
            _credFormStatusTypeRepository = credFormStatusTypeRepository;
        }

        public async Task<List<ProviderByDelegateResponseDto>> Handle(GetProvidersByDelegateIdQuery request, CancellationToken cancellationToken)
        {
            return await GetProviderByDelegateId(request.DelegateId);
        }

        private async Task<List<ProviderByDelegateResponseDto>> GetProviderByDelegateId(int delegateId)
        {
            List<ProviderByDelegateResponseDto> result = new();

            _ = await _delegateRepository.GetByIdAsync(delegateId)
                ?? throw new DelegateNotFoundException($"Delegate was not found by id [{delegateId}].");

            var providerDelegates = await _providerDelegatesRepository.SearchByDelegateIdAsync(delegateId)
                ?? throw new GenericDelegateException($"No delegate [${delegateId}] associated with provider found.");

            foreach (var providerDelegate in providerDelegates)
            {
                var provider = providerDelegate.Provider;
                var credFormStatusTypeId = (await _credFormRepository.GetByIdAsync(provider.CredFormId))?.CredFormStatusTypeId;
                CredFormStatusTypeEntity credFormStatusTypeEntity = new();
                if (credFormStatusTypeId != null)
                {
                    credFormStatusTypeEntity = await _credFormStatusTypeRepository.GetByIdAsync(credFormStatusTypeId);
                }
                var resultItem = new ProviderByDelegateResponseDto
                {
                    ProviderId = provider.Id,
                    Name = provider.FirstName,
                    MiddleName = provider.MiddleName,
                    LastName = provider.LastName,
                    SurName = provider.SurName,
                    Email = provider.Email,
                    PhoneNumber = provider.PhoneNumber,
                    RenderingNPI = provider.RenderingNPI,
                    BillingNPI = provider.BillingNPI,
                    StatusName = credFormStatusTypeEntity?.Name,
                    PrioritySorting = credFormStatusTypeEntity?.PrioritySorting ?? 0
                };

                result.Add(resultItem);

            }

            var resultSorted = result.OrderBy(x => x.PrioritySorting).ToList();

            return resultSorted;
        }
    }
}
