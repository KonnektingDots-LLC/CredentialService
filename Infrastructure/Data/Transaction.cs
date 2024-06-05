using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore.Storage;

namespace cred_system_back_end_app.Infrastructure.Data
{
    public class Transaction : ITransaction
    {
        private readonly DbContextEntity _dbContextEntity;

        public Transaction(DbContextEntity dbContextEntity)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return await _dbContextEntity.Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
