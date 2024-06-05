using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Behavior
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ITransactionPipeline
    {
        private readonly ITransaction _transaction;
        private readonly ILoggerService<TransactionBehavior<TRequest, TResponse>> _loggerService;

        public TransactionBehavior(ITransaction transaction, ILoggerService<TransactionBehavior<TRequest, TResponse>> loggerService)
        {
            _transaction = transaction;
            _loggerService = loggerService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _loggerService.Info($"Begining transaction for {typeof(TRequest)}");
            await using var transaction = await _transaction.BeginTransactionAsync(cancellationToken);

            try
            {
                var result = await next();

                _loggerService.Info($"Commiting transaction for {typeof(TRequest)}");
                await transaction.CommitAsync(cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _loggerService.Error($"Rollback transaction for {typeof(TRequest)} because {ex.Message}", ex);
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            finally
            {
                _loggerService.Info($"End transaction for {typeof(TRequest)}");
            }
        }
    }
}
