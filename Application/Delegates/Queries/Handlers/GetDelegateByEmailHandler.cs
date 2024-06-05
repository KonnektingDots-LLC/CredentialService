using AutoMapper;
using cred_system_back_end_app.Application.DTO;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Delegates.Queries.Handlers
{
    public class GetDelegateByEmailHandler : IRequestHandler<GetDelegateByEmailQuery, DelegateResponseDto?>
    {
        private readonly IDelegateRepository _delegateRepository;
        private readonly IMapper _mapper;

        public GetDelegateByEmailHandler(IDelegateRepository delegateRepository, IMapper mapper)
        {
            _delegateRepository = delegateRepository;
            _mapper = mapper;
        }

        public async Task<DelegateResponseDto?> Handle(GetDelegateByEmailQuery request, CancellationToken cancellationToken)
        {
            return await GetDelegateByEmail(request.DelegateEmail);
        }

        public async Task<DelegateResponseDto?> GetDelegateByEmail(string email)
        {
            DelegateEntity? delegat = await _delegateRepository.GetWithRelatedRefByEmailAsync(email)
                ?? throw new DelegateNotFoundException("Delegate was not found by email address.");
            var result = _mapper.Map<DelegateResponseDto>(delegat);

            var delegateType = delegat.DelegateType;
            DelegateTypeDto delegType = new()
            {
                Id = delegateType.Id,
                Name = delegateType?.Name,
                IsActive = delegateType?.IsActive ?? false,
                IsExpired = delegateType?.IsExpired ?? false,
                ExpiredDate = delegateType?.ExpiredDate
            };

            var delegateCompany = delegat.DelegateCompany;
            DelegateCompanyDto delegCompany = new()
            {
                Name = delegateCompany?.Name,
                RepresentativeFullName = delegateCompany?.RepresentativeFullName,
                RepresentativeEmail = delegateCompany?.RepresentativeEmail,
                IsActive = delegateCompany?.IsActive ?? false
            };

            var delegateProviders = delegat.ProviderDelegate;
            List<ProviderDelegateDto> providers = new();
            if (delegateProviders != null && delegateProviders.Any())
            {
                foreach (var delegateProvider in delegateProviders)
                {
                    ProviderDelegateDto newProvider = new()
                    {
                        ProviderId = delegateProvider.ProviderId,
                        IsActive = delegateProvider.IsActive
                    };
                    providers.Add(newProvider);
                }
            }

            result.DelegateType = delegType;
            result.DelegateCompany = delegCompany;
            result.ProviderDelegate = providers;

            return result;
        }
    }
}
