using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    [Serializable]
    internal class ProviderTypeNotFoundException : Exception
    {
        public ProviderTypeNotFoundException()
        {
        }

        public ProviderTypeNotFoundException(string? message) : base(message)
        {
        }

        public ProviderTypeNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ProviderTypeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}