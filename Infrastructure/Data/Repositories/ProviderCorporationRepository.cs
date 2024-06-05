using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class ProviderCorporationRepository : GenericAuditRepository<ProviderCorporationEntity, int>, IProviderCorporationRepository
    {
        private readonly DbContextEntity _context;

        public ProviderCorporationRepository(DbContextEntity context, IHttpContextAccessor contextAccessor) : base(context, contextAccessor)
        {
            _context = context;
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
