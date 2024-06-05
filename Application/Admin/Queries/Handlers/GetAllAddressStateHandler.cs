using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Queries.Handlers
{
    public class GetAllAddressStateHandler : IRequestHandler<GetAllAddressStateQuery, List<AddressStateResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<AddressStateEntity, int> _addressStateRepository;

        public GetAllAddressStateHandler(IGenericRepository<AddressStateEntity, int> addressStateRepository, IMapper mapper)
        {
            _addressStateRepository = addressStateRepository;
            _mapper = mapper;
        }

        public async Task<List<AddressStateResponseDto>> Handle(GetAllAddressStateQuery request, CancellationToken cancellationToken)
        {
            var uiList = await _addressStateRepository.ListAsync();
            if (uiList.Count == 0)
            {
                throw new AddressStateNotFoundException();
            }
            return _mapper.Map<List<AddressStateResponseDto>>(uiList);
        }
    }
}
