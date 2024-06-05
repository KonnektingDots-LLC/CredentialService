using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    [Serializable]
    internal class PlanAcceptNotFoundException : Exception
    {
        public PlanAcceptNotFoundException()
        {
        }

        public PlanAcceptNotFoundException(string? message) : base(message)
        {
        }

        public PlanAcceptNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PlanAcceptNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}