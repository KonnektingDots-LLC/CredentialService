using AutoMapper;
using cred_system_back_end_app.Application.DTO;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class ProviderInsurerCompanyStatusHistoryRepository : GenericAuditRepository<ProviderInsurerCompanyStatusHistoryEntity, int>, IProviderInsurerCompanyStatusHistoryRepository
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;

        public ProviderInsurerCompanyStatusHistoryRepository(DbContextEntity context, IHttpContextAccessor contextAccessor, IMapper mapper) : base(context, contextAccessor)
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

        public async Task<List<ProviderInsurerCompanyStatusHistoryEntity>> GetProviderInsurerCompanyStatusHistoryByProviderInsurerCompanyStatusId(int providerInsurerCompanyStatusId)
        {
            var providerInsurerCompanyStatusHistoryEntity = await _context.ProviderInsurerCompanyStatusHistory
                                                    .Where(r => r.ProviderInsurerCompanyStatusId == providerInsurerCompanyStatusId).ToListAsync();

            if (providerInsurerCompanyStatusHistoryEntity.Count <= 0) { throw new EntityNotFoundException(); }
            return providerInsurerCompanyStatusHistoryEntity;
        }

    }
}
