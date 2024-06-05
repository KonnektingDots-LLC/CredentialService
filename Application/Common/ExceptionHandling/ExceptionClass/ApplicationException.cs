using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    /// <summary>
    /// A generic exception to describe Application specific errors (known exceptions).
    /// </summary>
    [Serializable]
    public class ApplicationException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationException"/> class.
        /// </summary>
        public ApplicationException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        public ApplicationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        /// <param name="inner">The original exception that caused this one.</param>
        public ApplicationException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Serialization constructor.
        /// </summary>
        /// <param name="info">Data storage for serialization data.</param>
        /// <param name="context">Describes the source and destination stream.</param>
        protected ApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
