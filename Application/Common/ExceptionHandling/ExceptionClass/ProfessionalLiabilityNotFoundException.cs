using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    [Serializable]
    internal class ProfessionalLiabilityNotFoundException : Exception
    {
        public ProfessionalLiabilityNotFoundException()
        {
        }

        public ProfessionalLiabilityNotFoundException(string? message) : base(message)
        {
        }

        public ProfessionalLiabilityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ProfessionalLiabilityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}