using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    public class EdResidencyInstitutionNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="EdResidencyInstitutionNotFoundException"/> class.
        /// </summary>
        public EdResidencyInstitutionNotFoundException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="EdResidencyInstitutionNotFoundException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        public EdResidencyInstitutionNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="EdResidencyInstitutionNotFoundException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        /// <param name="inner">The original exception that caused this one.</param>
        public EdResidencyInstitutionNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Serialization constructor.
        /// </summary>
        /// <param name="info">Data storage for serialization data.</param>
        /// <param name="context">Describes the source and destination stream.</param>
        protected EdResidencyInstitutionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
