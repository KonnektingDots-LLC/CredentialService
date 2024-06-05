using System.Runtime.Serialization;

namespace cred_system_back_end_app.Domain.Exceptions
{
    /// <summary>
    /// It is thrown when an entity is not found when trying to do an operation on it.
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        public EntityNotFoundException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        public EntityNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        /// <param name="inner">The original exception that caused this one.</param>
        public EntityNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Serialization constructor.
        /// </summary>
        /// <param name="info">Data storage for serialization data.</param>
        /// <param name="context">Describes the source and destination stream.</param>
        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
