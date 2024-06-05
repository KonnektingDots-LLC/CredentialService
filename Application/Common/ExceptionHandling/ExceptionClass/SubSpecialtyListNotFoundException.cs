using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    public class SubSpecialtyListNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="SubSpecialtyListNotFoundException"/> class.
        /// </summary>
        public SubSpecialtyListNotFoundException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SubSpecialtyListNotFoundException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        public SubSpecialtyListNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SubSpecialtyListNotFoundException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        /// <param name="inner">The original exception that caused this one.</param>
        public SubSpecialtyListNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Serialization constructor.
        /// </summary>
        /// <param name="info">Data storage for serialization data.</param>
        /// <param name="context">Describes the source and destination stream.</param>
        protected SubSpecialtyListNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
