using Microsoft.EntityFrameworkCore.Storage;

namespace cred_system_back_end_app.Domain.Interfaces
{
    public interface ITransaction
    {
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    }
}
