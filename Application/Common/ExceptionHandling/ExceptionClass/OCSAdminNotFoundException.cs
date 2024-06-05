using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    [Serializable]
    internal class OCSAdminNotFoundException : Exception
    {
        public OCSAdminNotFoundException()
        {
        }

        public OCSAdminNotFoundException(string? message) : base(message)
        {
        }

        public OCSAdminNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected OCSAdminNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}