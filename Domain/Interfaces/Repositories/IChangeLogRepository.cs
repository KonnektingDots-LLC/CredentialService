namespace cred_system_back_end_app.Domain.Interfaces.Repositories
{
    public interface IChangeLogRepository
    {
        Task AddLogToContext
        (
            string changedBy,
            string useCaseType,
            string resourceType,
            string resourceId,
            string? useCaseTargetId = null,
            string? oldValue = null,
            string? newValue = null
        );
    }
}
