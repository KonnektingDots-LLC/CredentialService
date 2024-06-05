using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class AttestationRepository : GenericAuditRepository<AttestationEntity, int>, IAttestationRepository
    {
        private readonly DbContextEntity _dbContextEntity;

        public AttestationRepository(DbContextEntity dbContextEntity, IHttpContextAccessor contextAccessor) : base(dbContextEntity, contextAccessor)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task<AttestationEntity> GetAttestationByProviderIdAsync(int providerId)
        {
            return await _dbContextEntity.Attestation
                .Where(a => a.ProviderId == providerId)
                .Include(a => a.Provider)
                .FirstOrDefaultAsync()
                ?? throw new EntityNotFoundException();
        }
    }
}
