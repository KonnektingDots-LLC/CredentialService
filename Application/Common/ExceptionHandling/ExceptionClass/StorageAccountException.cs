using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    /// <summary>
    /// A generic exception to describe State Hub specific errors (known exceptions).
    /// </summary>
    [Serializable]
    public class StorageAccountException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationException"/> class.
        /// </summary>
        public StorageAccountException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        public StorageAccountException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        /// <param name="inner">The original exception that caused this one.</param>
        public StorageAccountException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Serialization constructor.
        /// </summary>
        /// <param name="info">Data storage for serialization data.</param>
        /// <param name="context">Describes the source and destination stream.</param>
        protected StorageAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
