using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    [Serializable]
    internal class InsurerEmployeeDuplicateException : Exception
    {
        public InsurerEmployeeDuplicateException()
        {
        }

        public InsurerEmployeeDuplicateException(string? message) : base(message)
        {
        }

        public InsurerEmployeeDuplicateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InsurerEmployeeDuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}