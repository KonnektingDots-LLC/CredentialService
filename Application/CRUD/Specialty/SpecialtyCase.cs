using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.CRUD.Specialty
{
    public class SpecialtyCase
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;


        public SpecialtyCase(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<SpecialtyResponseDto> GetSpecialtyByOrganizationId(int organizationId)
        {

            var specialty = _context.SpecialtyList
                            .Where(
                                    sl => sl.OrganizationTypeId == organizationId 
                                    && sl.IsActive == true
                                    && sl.IsExpired == false
                                    )
                            .OrderBy(sl => sl.Name)
                            .ToList();

            if (specialty.Count == 0)
            {

                throw new SpecialtyNotFoundException();
            }

            return _mapper.Map<List<SpecialtyResponseDto>>(specialty);


        }

    }
}
