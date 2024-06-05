using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    [Serializable]
    internal class AddressStateNotFoundException : Exception
    {
        public AddressStateNotFoundException()
        {
        }

        public AddressStateNotFoundException(string? message) : base(message)
        {
        }

        public AddressStateNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AddressStateNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}