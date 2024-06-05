using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    [Serializable]
    internal class MalpracticeNotFoundException : Exception
    {
        public MalpracticeNotFoundException()
        {
        }

        public MalpracticeNotFoundException(string? message) : base(message)
        {
        }

        public MalpracticeNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MalpracticeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}