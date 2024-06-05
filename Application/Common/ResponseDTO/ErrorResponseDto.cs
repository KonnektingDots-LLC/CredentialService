namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class ErrorResponseDto
    {
        public int Code { get; set; }
        public string Message { get; set; }
        //public int StatusCode { get; set; }

        public string ErrorDetails { get; set; }
    }
}
