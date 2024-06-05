using Azure;
using cred_system_back_end_app.Application.Common.ExceptionHandling;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionList;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Extensions;
using System;
using System.Net;
using System.Reflection.Metadata;
using System.Text.Json;

namespace cred_system_back_end_app.Controllers
{
    public class ExceptionHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //context.Response.ContentType = "application/json";

            ErrorResponseDto errorResponseDto = null;
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
                        //StatusCode = errorInfoAttribute.ErrorStatusCode
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
                    };
                }
            }

            errorResponseDto.ErrorDetails = $"{context.Exception.Message} InnerException: {context.Exception?.InnerException?.Message}";

            var response = new ObjectResult(new { errorResponseDto}) { StatusCode = statusCode};
            context.Result = response; 
            context.ExceptionHandled = true;
        }
    }

}
