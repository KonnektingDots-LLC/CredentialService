using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.CRUD.ChangeLogservices
{
    public class ChangeLogRepository
    {
        private readonly DbContextEntity _dbContextEntity;

        public ChangeLogRepository(DbContextEntity dbContextEntity)
        {
            _dbContextEntity = dbContextEntity;
        }

        public void AddLogToContext
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

            _dbContextEntity.Add(changeLog);
        }
    }
}
