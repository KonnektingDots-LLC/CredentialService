using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.CRUD.SubSpecialty
{
    public class SubSpecialtyRepository
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;


        public SubSpecialtyRepository(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }        
        
        public SubSpecialtyRepository(DbContextEntity context)
        {
            _context = context;
        }


        public List<SubSpecialtyResponseDto> GetSubSpecialtyByOrganizationId(int organizationId)
        {

            var Subspecialty = _context.SubSpecialtyList.Where(sl => sl.OrganizationTypeId == organizationId).ToList();
            if (Subspecialty.Count == 0)
            {

                throw new SubSpecialtyNotFoundException();
            }

            return _mapper.Map<List<SubSpecialtyResponseDto>>(Subspecialty);

        }

        public SubSpecialtyListEntity GetSubSpecialtyByProviderId(int providerId)
        {

            var providerSubSpecialtyTypeId = _context.ProviderSubSpecialty
                .Where(subSpecialty => subSpecialty.ProviderId == providerId)
                .ToList()
                .First()
                .SubSpecialtyListId;

            return _context.SubSpecialtyList.Find(providerSubSpecialtyTypeId);
        }

    }

}
