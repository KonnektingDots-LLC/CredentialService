using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Queries.Handlers
{
    public class GetAllHospitalListHandler : IRequestHandler<GetAllHospitalListQuery, List<HospitalListResponseDto>>
    {
        private readonly IGenericRepository<HospitalListEntity, int> _genericRepository;
        private readonly IMapper _mapper;

        public GetAllHospitalListHandler(IGenericRepository<HospitalListEntity, int> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<List<HospitalListResponseDto>> Handle(GetAllHospitalListQuery request, CancellationToken cancellationToken)
        {
            var hospitals = await _genericRepository.ListAsync();
            if (hospitals.Count == 0)
            {
                throw new HospitalNotFoundException();
            }
            return _mapper.Map<List<HospitalListResponseDto>>(hospitals);
        }
    }
}
