using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.CRUD.Corporation.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDFMapper = cred_system_back_end_app.Application.Common.Mappers.EntityToPDF;

namespace cred_system_back_end_app.Application.UseCase.Corporation
{
    public class CorporationRepository
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;

        public CorporationRepository(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }        
        
        public CorporationRepository(DbContextEntity context)
        {
            _context = context;
        }

        public List<CorporationResponseDto> GetAllCorporations()
        {
            var corporations = _context.Corporation.ToList();
            return _mapper.Map<List<CorporationResponseDto>>(corporations);
        }

        public CorporationResponseDto? GetCorporationById(int id)
        {

            var corporation = _context.Corporation.Find(id);
            if (corporation == null)
            {

                throw new CorporationNotFoundException();
            }

            return _mapper.Map<CorporationResponseDto>(corporation);


        }

        public CorporatePracticeProfile2Dto GetCorporatePracticeProfile(int providerId)
        {
            var providerCorporation = _context.ProviderCorporation
                .Where(providerCorporation => providerCorporation.ProviderId == providerId)
                .ToList()
                .FirstOrDefault();

            var corporation = _context.Corporation.Find(providerCorporation.CorporationId);

            var entityType = _context.EntityType.Find(corporation.EntityTypeId);

            return PDFMapper.Corporation.GetCorporatePracticeProfile2DTO(corporation);
        }

        public CorporationResponseDto CreateCorporation([FromBody] CreateCorporationDto createDto)
        {
            try
            {

                var newCorporation = _mapper.Map<CorporationEntity>(createDto);
                //newCorporation.CreationDate = DateTime.Now;

                _context.Corporation.Add(newCorporation);
                _context.SaveChanges();

                var newCorporationResponse = _mapper.Map<CorporationResponseDto>(newCorporation);

                return newCorporationResponse;


            }
            catch (Exception ex) { throw; }
        }

        public CorporationResponseDto UpdateCorporation(CorporationEntity corporationEntity)
        {
            _context.Entry(corporationEntity).State = EntityState.Modified;
            _context.SaveChanges();

            if (corporationEntity == null)
            {
                throw new EntityNotFoundException();
            }

            var updateCorporationResponse = _mapper.Map<CorporationResponseDto>(corporationEntity);

            return updateCorporationResponse;
        }

        public async Task<List<ProviderCorporationEntity>> GetProviderCorporationsByProviderId(int providerId) 
        {
            return await _context.ProviderCorporation
                .Where(c => c.ProviderId == providerId)
                .Include(c => c.Corporation.Address)
                .Include(c => c.Corporation)
                .ThenInclude(c => c.CorporationDocument)
                .ToListAsync();
        }
    }
}
