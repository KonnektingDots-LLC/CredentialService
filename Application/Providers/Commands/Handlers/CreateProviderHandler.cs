using AutoMapper;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Commands.Handlers
{
    public class CreateProviderHandler : IRequestHandler<CreateProviderCommand, CreateCredFormProviderResponseDto>
    {
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;

        public CreateProviderHandler(IProviderService providerService, IMapper mapper)
        {
            _providerService = providerService;
            _mapper = mapper;
        }

        public async Task<CreateCredFormProviderResponseDto> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
        {
            var newProviderEntity = _mapper.Map<ProviderEntity>(request.CreateProviderRequestDto);
            var (savedProviderEntity, savedCredForm) = await _providerService.CreateProviderWithNewCredFormVersion(newProviderEntity);

            var credFormResponse = _mapper.Map<CredFormResponseDto>(savedCredForm);
            credFormResponse.State = savedCredForm.CredFormStatusType.StateTypeId;

            var providerResponse = _mapper.Map<CreatedProviderResponseDto>(savedProviderEntity);

            CreateCredFormProviderResponseDto createCredFormProviderResponseDto = new CreateCredFormProviderResponseDto()
            {
                CredFormResponse = credFormResponse,
                CreatedProviderResponse = providerResponse
            };
            return createCredFormProviderResponseDto;
        }

    }
}
