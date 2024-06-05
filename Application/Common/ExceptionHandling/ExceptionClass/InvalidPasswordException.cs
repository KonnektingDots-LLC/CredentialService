using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    /// <summary>
    /// An exception for wrong passwords.
    /// </summary>
    [Serializable]
    public class InvalidPasswordException : ApplicationException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="InvalidPasswordException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        public InvalidPasswordException(string message = null) : base("Invalid password." + (string.IsNullOrWhiteSpace(message) ? string.Empty : $" {message}")) { }

        /// <inheritdoc/>
        protected InvalidPasswordException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
