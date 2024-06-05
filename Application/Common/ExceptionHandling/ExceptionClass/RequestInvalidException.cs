using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    /// <summary>
    /// An exception to describe App request vaidation errors
    /// </summary>
    [Serializable]
    public class RequestInvalidException : ApplicationException
    {
        /// <summary>
        /// Creates and instance of <see cref="RequestInvalidException" />
        /// </summary>
        public RequestInvalidException()
        {

        }

        /// <inheritdoc />
        public RequestInvalidException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Serialization constructor.
        /// </summary>
        /// <param name="info">Data storage for serialization data.</param>
        /// <param name="context">Describes the source and destination stream.</param>
        protected RequestInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
