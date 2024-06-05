using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.CRUD.IIPCAFormSections
{
    public class AttestationRepository
    {
        private DbContextEntity _dbContextEntity;

        public AttestationRepository(DbContextEntity dbContextEntity) 
        { 
            _dbContextEntity = dbContextEntity;
        }

        public void SetDbContextTransaction(DbContextEntity dbContextEntity)
        {
            _dbContextEntity = dbContextEntity;
        }

        public AttestationEntity GetAttestationByProviderIdAsync(int providerId) 
        { 
            var attestationEntity = _dbContextEntity.Attestation
                .Where(a => a.ProviderId == providerId)
                .Include(a => a.Provider)
                .FirstOrDefault();

            if (attestationEntity == null) { throw new EntityNotFoundException(); }

            return attestationEntity;
        }
    }
}
