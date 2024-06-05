namespace cred_system_back_end_app.Domain.Exceptions
{
    public class GenericProviderException : Exception
    {
        public GenericProviderException(string? message) : base(message)
        {
        }

        public GenericProviderException(string message, Exception inner) : base(message, inner) { }
    }
}
