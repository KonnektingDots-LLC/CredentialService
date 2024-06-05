namespace cred_system_back_end_app.Application.Common.ExceptionHandling
{
    public class DateTimeParsingErrorException : Exception
    {
        public DateTimeParsingErrorException() 
        { 
        
        }

        public DateTimeParsingErrorException(string message) 
            : base(message) 
        { 
        
        }
    }
}
