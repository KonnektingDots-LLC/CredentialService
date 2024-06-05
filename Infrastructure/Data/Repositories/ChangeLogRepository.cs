using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class ChangeLogRepository : IChangeLogRepository
    {
        private readonly DbContextEntity _dbContextEntity;

        public ChangeLogRepository(DbContextEntity dbContextEntity)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task AddLogToContext
        (
            string changedBy,
            string useCaseType,
            string resourceType,
            string resourceId,
            string? useCaseTargetId = null,
            string? oldValue = null,
            string? newValue = null
        )
        {
            var changeLog = new ChangeLogEntity
            {
                ChangeLogUseCaseTypeId = useCaseType,
                ChangeLogResourceTypeId = resourceType,
                ChangeLogResourceId = resourceId,
                ChangeLogUseCaseTargetId = useCaseTargetId,
                OldValue = oldValue,
                NewValue = newValue,
                ChangedBy = changedBy,
                ChangedDate = DateTime.Now,
            };

            await _dbContextEntity.AddAsync(changeLog);
            await _dbContextEntity.SaveChangesAsync();
        }
    }
}
