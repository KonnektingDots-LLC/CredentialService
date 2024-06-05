using AutoMapper;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Queries.Handlers
{
    public class GetLatestCredentialingFormSnapshotHanlders : IRequestHandler<GetLatestCredentialingFormSnapshotQuery, CredentialingFormSnapshotResponseDto>
    {
        private readonly ICrendentialingFormService _crendentialingFormService;
        private readonly IMapper _mapper;

        public GetLatestCredentialingFormSnapshotHanlders(ICrendentialingFormService crendentialingFormService, IMapper mapper)
        {
            _crendentialingFormService = crendentialingFormService;
            _mapper = mapper;
        }

        public async Task<CredentialingFormSnapshotResponseDto> Handle(GetLatestCredentialingFormSnapshotQuery request, CancellationToken cancellationToken)
        {
            JsonProviderFormEntity jsonProviderFormEntity = await _crendentialingFormService.GetLatestSnapshotByProviderId(request.ProviderId);
            CredentialingFormSnapshotResponseDto jsonProvider = _mapper.Map<CredentialingFormSnapshotResponseDto>(jsonProviderFormEntity);
            return jsonProvider;
        }
    }
}
