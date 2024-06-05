namespace cred_system_back_end_app.Domain.Exceptions
{
    public class GenericInsurerException : Exception
    {
        public GenericInsurerException(string? message) : base(message) { }
        public GenericInsurerException(string message, Exception inner) : base(message, inner) { }
    }
}
