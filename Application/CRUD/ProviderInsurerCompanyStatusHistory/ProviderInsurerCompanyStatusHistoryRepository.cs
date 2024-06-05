using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatusHistory.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatusHistory
{
    public class ProviderInsurerCompanyStatusHistoryRepository
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;

        public ProviderInsurerCompanyStatusHistoryRepository(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProviderInsurerCompanyStatusHistoryResponseDto> 
            CreateProviderInsurerCompanyStatusHistory(ProviderInsurerCompanyStatusHistoryDto ProviderInsurerCompanyStatusHistory)
        {

            var newProviderInsurerCompanyStatusHistory = _mapper.Map<ProviderInsurerCompanyStatusHistoryEntity>(ProviderInsurerCompanyStatusHistory);

            _context.ProviderInsurerCompanyStatusHistory.Add(newProviderInsurerCompanyStatusHistory);
            _context.SaveChanges();

            var newProviderInsurerCompanyStatusHistoryResponse = _mapper.Map<ProviderInsurerCompanyStatusHistoryResponseDto>(newProviderInsurerCompanyStatusHistory);


            return newProviderInsurerCompanyStatusHistoryResponse;
        }

        public ProviderInsurerCompanyStatusHistoryEntity
        AddProviderInsurerCompanyStatusHistory(ProviderInsurerCompanyStatusHistoryDto ProviderInsurerCompanyStatusHistory)
        {

            var newProviderInsurerCompanyStatusHistory = _mapper.Map<ProviderInsurerCompanyStatusHistoryEntity>(ProviderInsurerCompanyStatusHistory);

            return newProviderInsurerCompanyStatusHistory;
        }

        public async Task<List<ProviderInsurerCompanyStatusHistoryEntity>> GetProviderInsurerCompanyStatusHistoryByProviderInsurerCompanyStatusId(int providerInsurerCompanyStatusId)
        {

            var providerInsurerCompanyStatusHistoryEntity = await _context.ProviderInsurerCompanyStatusHistory
                                                    .Where(r => r.ProviderInsurerCompanyStatusId == providerInsurerCompanyStatusId).ToListAsync();

            if (providerInsurerCompanyStatusHistoryEntity.Count <= 0) { throw new EntityNotFoundException(); }

            return providerInsurerCompanyStatusHistoryEntity;
        }

        public async Task<ProviderInsurerCompanyStatusHistoryEntity> GetProviderInsurerCompanyStatusHistory(int id)
        {

            var providerInsurerCompanyStatusHistoryEntity = await _context.ProviderInsurerCompanyStatusHistory.FindAsync(id);

            if (providerInsurerCompanyStatusHistoryEntity == null) { throw new EntityNotFoundException(); }

            return providerInsurerCompanyStatusHistoryEntity;
        }
    }
}
