

namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class APIResponse<T>
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        public T Body { get; set; }
        public ErrorResponseDto? Error { get; set; }
    }

    public static class APIResponseHelper<T>
    {
        public static APIResponse<T> ErrorResponse(string message, ErrorResponseDto error)
        {

            return new APIResponse<T> { Successful = false, Error = error, Message = message };
        }


        public static APIResponse<T> SuccessfulResponse(T body, string message)
        {
            return new APIResponse<T> { Successful = true, Body = body, Message = message };
        }
    }
}
