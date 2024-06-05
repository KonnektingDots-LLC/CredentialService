using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Queries.Handlers
{
    public class GetAllInsuranceMalpracticeCarrierHandler : IRequestHandler<GetAllInsuranceMalpracticeCarrierQuery, List<InsuranceCarrierListResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<MalpracticeCarrierListEntity, int> _genericRepository;

        public GetAllInsuranceMalpracticeCarrierHandler(IMapper mapper, IGenericRepository<MalpracticeCarrierListEntity, int> genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }

        public async Task<List<InsuranceCarrierListResponseDto>> Handle(GetAllInsuranceMalpracticeCarrierQuery request, CancellationToken cancellationToken)
        {
            var malpracticeCarriers = await _genericRepository.ListAsync();
            if (malpracticeCarriers.Count == 0)
            {
                throw new MalpracticeNotFoundException();
            }
            return _mapper.Map<List<InsuranceCarrierListResponseDto>>(malpracticeCarriers);
        }
    }
}
