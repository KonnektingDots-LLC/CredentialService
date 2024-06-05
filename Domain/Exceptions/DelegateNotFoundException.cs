using System.Runtime.Serialization;

namespace cred_system_back_end_app.Domain.Exceptions
{
    [Serializable]
    public class DelegateNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DelegateNotFoundException"/> class.
        /// </summary>
        public DelegateNotFoundException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DelegateNotFoundException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        public DelegateNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DelegateNotFoundException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        /// <param name="inner">The original exception that caused this one.</param>
        public DelegateNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Serialization constructor.
        /// </summary>
        /// <param name="info">Data storage for serialization data.</param>
        /// <param name="context">Describes the source and destination stream.</param>
        protected DelegateNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
