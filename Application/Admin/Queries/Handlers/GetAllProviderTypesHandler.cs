using AutoMapper;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Queries.Handlers
{
    public class GetAllProviderTypesHandler : IRequestHandler<GetAllProviderTypesQuery, List<ListResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProviderTypeEntity, int> _providerTypeEntityRepository;

        public GetAllProviderTypesHandler(IGenericRepository<ProviderTypeEntity, int> providerTypeEntityRepository, IMapper mapper)
        {
            _providerTypeEntityRepository = providerTypeEntityRepository;
            _mapper = mapper;
        }

        public async Task<List<ListResponseDto>> Handle(GetAllProviderTypesQuery request, CancellationToken cancellationToken)
        {
            var uiList = await _providerTypeEntityRepository.ListAsync();
            return _mapper.Map<List<ListResponseDto>>(uiList);
        }
    }
}
