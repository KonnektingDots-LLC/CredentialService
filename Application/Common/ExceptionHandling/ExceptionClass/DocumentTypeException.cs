using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    [Serializable]
    internal class DocumentTypeException : Exception
    {
        public DocumentTypeException()
        {
        }

        public DocumentTypeException(string? message) : base(message)
        {
        }

        public DocumentTypeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DocumentTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}