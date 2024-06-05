using AutoMapper;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Queries.Handlers
{
    public class GetCredentialingFormStatusByProviderEmailHandler : IRequestHandler<GetCredentialingFormStatusByProviderEmailQuery, CredFormResponseDto>
    {
        private readonly ICredFormRepository _credFormRepository;
        private readonly IGenericRepository<CredFormStatusTypeEntity, string> _credFormStatusTypeRepository;
        private readonly IGenericRepository<StateTypeEntity, string> _stateTypeRepository;
        private readonly IMapper _mapper;

        public GetCredentialingFormStatusByProviderEmailHandler(ICredFormRepository credFormRepository,
            IGenericRepository<CredFormStatusTypeEntity, string> credFormStatusTypeRepository,
            IGenericRepository<StateTypeEntity, string> stateTypeRepository, IMapper mapper)
        {
            _credFormRepository = credFormRepository;
            _stateTypeRepository = stateTypeRepository;
            _mapper = mapper;
            _credFormStatusTypeRepository = credFormStatusTypeRepository;
        }

        public async Task<CredFormResponseDto> Handle(GetCredentialingFormStatusByProviderEmailQuery request, CancellationToken cancellationToken)
        {
            var credForm = await _credFormRepository.GetByEmailAsync(request.ProviderEmail)
                ?? throw new GenericCredentialingFormException("Credentialing form status was not found by provider email.");

            var credFormStatus = await _credFormStatusTypeRepository.GetByIdAsync(credForm.CredFormStatusTypeId)
                ?? throw new EntityNotFoundException($"Credentialing form status type was not found by id [{credForm.CredFormStatusTypeId}]");

            var state = await _stateTypeRepository.GetByIdAsync(credFormStatus.StateTypeId)
                ?? throw new EntityNotFoundException($"State type was not found by id [{credFormStatus.StateTypeId}]");

            var credFormResponse = _mapper.Map<CredFormResponseDto>(credForm);
            credFormResponse.State = state.Id;
            return credFormResponse;
        }
    }
}
