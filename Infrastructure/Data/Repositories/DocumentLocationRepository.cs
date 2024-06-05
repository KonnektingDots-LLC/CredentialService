using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class DocumentLocationRepository : IDocumentLocationRepository
    {
        private readonly DbContextEntity _dbContextEntity;
        private readonly IHttpContextAccessor _contextAccessor;

        public DocumentLocationRepository(DbContextEntity dbContextEntity, IHttpContextAccessor contextAccessor)
        {
            _dbContextEntity = dbContextEntity;
            _contextAccessor = contextAccessor;
        }

        public async Task<DocumentLocationEntity> AddAndSaveAsync(DocumentLocationEntity entity)
        {
            entity.UploadDate ??= DateTime.Now;
            entity.UploadBy ??= GetLoggedUserEmail();

            await _dbContextEntity.DocumentLocation.AddAsync(entity);
            await _dbContextEntity.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(DocumentLocationEntity entity)
        {
            _dbContextEntity.DocumentLocation.Remove(entity);
            await _dbContextEntity.SaveChangesAsync();
        }

        public async Task<DocumentLocationEntity?> GetByProviderIdAndDocumentTypeAndUploadfilename(int providerId, int documentType, string uploadFilename)
        {
            return await _dbContextEntity.DocumentLocation
                    .Where(drf => drf.ProviderId == providerId
                    && drf.UploadFilename == uploadFilename
                    && drf.DocumentTypeId == documentType).FirstOrDefaultAsync();
        }

        public async Task<DocumentLocationEntity> UpdateAsync(DocumentLocationEntity entity)
        {
            entity.ModifiedDate ??= DateTime.Now;
            entity.ModifiedBy ??= GetLoggedUserEmail();
            await _dbContextEntity.SaveChangesAsync();

            return entity;
        }

        public string? GetLoggedUserEmail()
        {
            var httpContext = _contextAccessor.HttpContext;
            var user = httpContext?.User;

            return user?.FindFirst(CredTokenKey.EMAIL)?.Value ?? "anonymous";
        }
    }
}
