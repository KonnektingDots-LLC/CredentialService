using AutoMapper;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Queries.Handlers
{
    public class GetAllAcceptPlanListHandler : IRequestHandler<GetAllAcceptPlanListQuery, List<ListResponseDto>>
    {
        private readonly IGenericRepository<PlanAcceptListEntity, int> _planAcceptListEntityRepository;
        private readonly IMapper _mapper;

        public GetAllAcceptPlanListHandler(IGenericRepository<PlanAcceptListEntity, int> planAcceptListEntityRepository, IMapper mapper)
        {
            _planAcceptListEntityRepository = planAcceptListEntityRepository;
            _mapper = mapper;
        }

        public async Task<List<ListResponseDto>> Handle(GetAllAcceptPlanListQuery request, CancellationToken cancellationToken)
        {
            var uiList = await _planAcceptListEntityRepository.ListAsync();
            return _mapper.Map<List<ListResponseDto>>(uiList);
        }
    }
}
