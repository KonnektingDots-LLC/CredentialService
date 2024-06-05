using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Queries.Handlers
{
    public class GetAllAddressCountryHandler : IRequestHandler<GetAllAddressCountryQuery, List<ListResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<AddressCountryEntity, int> _addressCountryRepository;

        public GetAllAddressCountryHandler(IGenericRepository<AddressCountryEntity, int> addressCountryRepository, IMapper mapper)
        {
            _addressCountryRepository = addressCountryRepository;
            _mapper = mapper;
        }

        public async Task<List<ListResponseDto>> Handle(GetAllAddressCountryQuery request, CancellationToken cancellationToken)
        {
            var uiList = await _addressCountryRepository.ListAsync();
            if (uiList.Count == 0)
            {
                throw new AddressCountryNotFoundException();
            }
            return _mapper.Map<List<ListResponseDto>>(uiList);
        }
    }
}
