using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    /// <summary>
    /// It is thrown when a service tries to log and an error occurs.
    /// </summary>   
    [Serializable]
    public class LogException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="LogException" /> class.
        /// </summary>
        public LogException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LogException" /> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        public LogException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LogException" /> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        /// <param name="innerException">The original exception that caused this one.</param>
        public LogException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Serialization constructor.
        /// </summary>
        /// <param name="info">Data storage for serialization data.</param>
        /// <param name="context">Describes the source and destination stream.</param>
        protected LogException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
