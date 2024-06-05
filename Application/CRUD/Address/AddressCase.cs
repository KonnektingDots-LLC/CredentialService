using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.CRUD.Address.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.CRUD.Address
{
    public class AddressCase
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;

        public AddressCase(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AddressTypeResponseDto> GetAllAddressType()
        {
            var addresss = _context.AddressType.ToList();
            if (addresss.Count == 0) { throw new AddressTypeNotFoundException(); };
            return _mapper.Map<List<AddressTypeResponseDto>>(addresss);
        }

        public List<AddressStateResponseDto> GetAllAddressState()
        {
            var addresss = _context.AddressState.ToList();
            if (addresss.Count == 0) { throw new AddressStateNotFoundException(); };
            return _mapper.Map<List<AddressStateResponseDto>>(addresss);
        }

        public List<ListResponseDto> GetAllAddressCountry()
        {
            var addresss = _context.AddressCountry.ToList();
            if (addresss.Count == 0) { throw new AddressCountryNotFoundException(); };
            return _mapper.Map<List<ListResponseDto>>(addresss);
        }

    }
}
