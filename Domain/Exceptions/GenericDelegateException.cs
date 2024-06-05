namespace cred_system_back_end_app.Domain.Exceptions
{
    public class GenericDelegateException : Exception
    {
        public GenericDelegateException(string? message) : base(message)
        {
        }

        public GenericDelegateException(string message, Exception inner) : base(message, inner) { }
    }
}
