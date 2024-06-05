using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    [Serializable]
    internal class HospitalPrivilegeNotFoundException : Exception
    {
        public HospitalPrivilegeNotFoundException()
        {
        }

        public HospitalPrivilegeNotFoundException(string? message) : base(message)
        {
        }

        public HospitalPrivilegeNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected HospitalPrivilegeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}