using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    [Serializable]
    internal class InsurerCompanyNotFoundException : Exception
    {
        public InsurerCompanyNotFoundException()
        {
        }

        public InsurerCompanyNotFoundException(string? message) : base(message)
        {
        }

        public InsurerCompanyNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InsurerCompanyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}