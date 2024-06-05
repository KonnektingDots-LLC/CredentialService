using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Queries.Handlers
{
    public class GetSpecialtyByOrganizationTypeIdHandler : IRequestHandler<GetSpecialtyByOrganizationTypeIdQuery, List<SpecialtyResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly ISpecialtyListRepository _specialtyListRepository;

        public GetSpecialtyByOrganizationTypeIdHandler(IMapper mapper, ISpecialtyListRepository specialtyListRepository)
        {
            _mapper = mapper;
            _specialtyListRepository = specialtyListRepository;
        }

        public async Task<List<SpecialtyResponseDto>> Handle(GetSpecialtyByOrganizationTypeIdQuery request, CancellationToken cancellationToken)
        {
            var specialty = await _specialtyListRepository.GetSpecialtyByOrganizationId(request.OrganizationTypeId);

            if (specialty.Count.Equals(0))
            {
                throw new SpecialtyNotFoundException();
            }

            return _mapper.Map<List<SpecialtyResponseDto>>(specialty);
        }
    }
}
