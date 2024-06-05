using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    [Serializable]
    internal class DeniedNewRecordException : Exception
    {
        public DeniedNewRecordException()
        {
        }

        public DeniedNewRecordException(string? message) : base(message)
        {
        }

        public DeniedNewRecordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DeniedNewRecordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}