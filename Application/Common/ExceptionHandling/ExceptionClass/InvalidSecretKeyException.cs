using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    /// <summary>
    /// An exception for failed authentication to an API of a local agency.
    /// </summary>
    [Serializable]
    public class InvalidSecretKeyException : ApplicationException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="InvalidSecretKeyException"/> class.
        /// </summary>
        public InvalidSecretKeyException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="InvalidSecretKeyException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        public InvalidSecretKeyException(string message) : base(string.IsNullOrWhiteSpace(message) ? "Invalid secret key." : message) { }

        /// <inheritdoc/>
        protected InvalidSecretKeyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
