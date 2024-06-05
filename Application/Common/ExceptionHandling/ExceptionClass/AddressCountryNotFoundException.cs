using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    [Serializable]
    internal class AddressCountryNotFoundException : Exception
    {
        public AddressCountryNotFoundException()
        {
        }

        public AddressCountryNotFoundException(string? message) : base(message)
        {
        }

        public AddressCountryNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AddressCountryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}