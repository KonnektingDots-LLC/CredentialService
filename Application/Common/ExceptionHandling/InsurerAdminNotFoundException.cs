using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling
{
    [Serializable]
    internal class InsurerAdminNotFoundException : Exception
    {
        public InsurerAdminNotFoundException()
        {
        }

        public InsurerAdminNotFoundException(string? message) : base(message)
        {
        }

        public InsurerAdminNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InsurerAdminNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}