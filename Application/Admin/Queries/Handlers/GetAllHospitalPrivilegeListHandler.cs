using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Queries.Handlers
{
    public class GetAllHospitalPrivilegeListHandler : IRequestHandler<GetAllHospitalPrivilegeListQuery, List<HospPrivilegeResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<HospPriviledgeListEntity, int> _genericRepository;

        public GetAllHospitalPrivilegeListHandler(IMapper mapper, IGenericRepository<HospPriviledgeListEntity, int> genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }

        public async Task<List<HospPrivilegeResponseDto>> Handle(GetAllHospitalPrivilegeListQuery request, CancellationToken cancellationToken)
        {
            var privileges = await _genericRepository.ListAsync();
            if (privileges.Count == 0)
            {
                throw new HospitalPrivilegeNotFoundException();
            }
            return _mapper.Map<List<HospPrivilegeResponseDto>>(privileges);
        }
    }
}
