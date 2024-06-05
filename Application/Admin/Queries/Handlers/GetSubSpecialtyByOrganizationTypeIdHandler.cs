using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Queries.Handlers
{
    public class GetSubSpecialtyByOrganizationTypeIdHandler : IRequestHandler<GetSubSpecialtyByOrganizationTypeIdQuery, List<SubSpecialtyResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly ISubSpecialtyListRepository _subSpecialtyListRepository;

        public GetSubSpecialtyByOrganizationTypeIdHandler(IMapper mapper, ISubSpecialtyListRepository subSpecialtyListRepository)
        {
            _mapper = mapper;
            _subSpecialtyListRepository = subSpecialtyListRepository;
        }

        public async Task<List<SubSpecialtyResponseDto>> Handle(GetSubSpecialtyByOrganizationTypeIdQuery request, CancellationToken cancellationToken)
        {
            var subSpecialty = await _subSpecialtyListRepository.GetSubSpecialtyByOrganizationId(request.OrganizationTypeId);

            if (subSpecialty.Count == 0)
            {
                throw new SubSpecialtyNotFoundException();
            }

            return _mapper.Map<List<SubSpecialtyResponseDto>>(subSpecialty);
        }
    }
}
