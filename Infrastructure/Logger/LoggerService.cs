using cred_system_back_end_app.Domain.Interfaces;
using Serilog;
using Serilog.Context;

namespace cred_system_back_end_app.Infrastructure.Logger
{
    public class LoggerService<T> : ILoggerService<T> where T : class
    {
        private readonly ILogger<T> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDiagnosticContext _diagnosticContext;

        public LoggerService(ILogger<T> logger, IHttpContextAccessor contextAccessor, IDiagnosticContext diagnosticContext)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
            _diagnosticContext = diagnosticContext;
        }

        public void Error(string message, Exception? exception)
        {
            using (LogContext.PushProperty("TransactionId", GetTransactionId()))
            {
                _logger.LogError(message, exception);
            }
        }

        public void Info(string message)
        {
            using (LogContext.PushProperty("TransactionId", GetTransactionId()))
            {
                _logger.LogInformation(message);
            }
        }
        public void Warn(string message)
        {
            using (LogContext.PushProperty("TransactionId", GetTransactionId()))
            {
                _logger.LogWarning(message);
            }
        }


        public string? GetTransactionId()
        {
            var httpContext = _contextAccessor.HttpContext;
            var transactionId = httpContext?.Request.Headers["x-transaction-id"][0] ?? "nologgertraceidfound";

            _diagnosticContext.Set("TransactionId", transactionId);
            return transactionId;
        }
    }
}
