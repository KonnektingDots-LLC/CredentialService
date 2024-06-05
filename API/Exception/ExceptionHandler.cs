using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace cred_system_back_end_app.API.Exception
{
    public class ExceptionHandler : IExceptionFilter
    {
        private readonly ILoggerService<ExceptionHandler> _loggerService;

        public ExceptionHandler(ILoggerService<ExceptionHandler> loggerService)
        {
            _loggerService = loggerService;
        }

        public void OnException(ExceptionContext context)
        {
            //context.Response.ContentType = "application/json";

            ErrorResponseDto? errorResponseDto = null;
            ErrorInfoAttribute? errorInfoAttribute = null;
            var statusCode = 500;

            ExceptionMapper errorResult;
            if (Enum.TryParse(context.Exception.GetType().Name, out errorResult))
            {
                errorInfoAttribute = typeof(ExceptionMapper)
                    .GetField(errorResult.ToString())
                    .GetCustomAttributes(typeof(ErrorInfoAttribute), false)
                    .FirstOrDefault() as ErrorInfoAttribute;

                if (errorInfoAttribute != null)
                {
                    errorResponseDto = new ErrorResponseDto
                    {
                        Message = errorInfoAttribute.ErrorMessage,
                        Code = (int)errorResult,
                        StatusCode = errorInfoAttribute.ErrorStatusCode
                    };
                    statusCode = errorInfoAttribute.ErrorStatusCode;
                }
            }
            else
            {
                errorInfoAttribute = typeof(ExceptionMapper)
                    .GetField(ExceptionMapper.Default.ToString())
                    .GetCustomAttributes(typeof(ErrorInfoAttribute), false)
                    .FirstOrDefault() as ErrorInfoAttribute;

                if (errorInfoAttribute != null)
                {
                    errorResponseDto = new ErrorResponseDto
                    {
                        Message = errorInfoAttribute.ErrorMessage,
                        Code = (int)ExceptionMapper.Default,
                        StatusCode = errorInfoAttribute.ErrorStatusCode
                    };
                }
            }

            CustomProblemDetails problemDetails = new()
            {
                Title = errorResponseDto?.Message,
                Status = errorResponseDto?.StatusCode,
                Code = errorResponseDto?.Code,
                Detail = $"{context.Exception.Message} caused By: {context.Exception?.InnerException?.Message}",
                TransactionId = _loggerService.GetTransactionId()
            };

            // log the error
            _loggerService.Error(problemDetails.Detail, context.Exception);

            context.Result = new ObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }
    }

    public class CustomProblemDetails : ProblemDetails
    {
        public int? Code { get; set; }
        public string? TransactionId { get; set; }
    }

}
