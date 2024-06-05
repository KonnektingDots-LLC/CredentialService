namespace cred_system_back_end_app.Domain.Exceptions
{
    public class GenericCredentialingFormException : Exception
    {
        public GenericCredentialingFormException(string? message) : base(message) { }
    }
}
