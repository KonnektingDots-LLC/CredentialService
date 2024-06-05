using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Behavior
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILoggerService<LoggingBehavior<TRequest, TResponse>> _loggerService;

        public LoggingBehavior(ILoggerService<LoggingBehavior<TRequest, TResponse>> loggerService)
        {
            _loggerService = loggerService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _loggerService.Info($"Handling {typeof(TRequest)}");

            var response = await next();

            _loggerService.Info($"Handled {typeof(TResponse)}");
            return response;
        }
    }
}
