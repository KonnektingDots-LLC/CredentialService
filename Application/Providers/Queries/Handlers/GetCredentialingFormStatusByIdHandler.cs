using AutoMapper;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Queries.Handlers
{
    public class GetCredentialingFormStatusByIdHandler : IRequestHandler<GetCredentialingFormStatusByIdQuery, ProviderCredFormResponseDto>
    {
        private readonly ICrendentialingFormService _crendentialingFormService;
        private readonly IMapper _mapper;

        public GetCredentialingFormStatusByIdHandler(ICrendentialingFormService crendentialingFormService, IMapper mapper)
        {
            _crendentialingFormService = crendentialingFormService;
            _mapper = mapper;
        }

        public async Task<ProviderCredFormResponseDto> Handle(GetCredentialingFormStatusByIdQuery request, CancellationToken cancellationToken)
        {
            CredFormEntity? credFormEntity = await _crendentialingFormService.GetById(request.credentialingFormId);
            var providerCredFormResponse = _mapper.Map<ProviderCredFormResponseDto>(credFormEntity?.Provider);
            providerCredFormResponse.Version = credFormEntity?.Version ?? 0;
            providerCredFormResponse.ProviderStatus = credFormEntity?.CredFormStatusTypeId;

            return providerCredFormResponse;
        }
    }
}
