using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    [Serializable]
    internal class AddressTypeNotFoundException : Exception
    {
        public AddressTypeNotFoundException()
        {
        }

        public AddressTypeNotFoundException(string? message) : base(message)
        {
        }

        public AddressTypeNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AddressTypeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}