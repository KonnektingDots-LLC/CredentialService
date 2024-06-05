using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass
{
    /// <summary>
    /// An exception for failed user authentication.
    /// </summary>
    [Serializable]
    public class AuthenticationException : ApplicationException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="AuthenticationException"/> class.
        /// </summary>
        /// <param name="name">The user name.</param>
        /// <param name="url">The url of the application.</param>
        public AuthenticationException(string name, string url) : base($"Authentication to the service '{url}' for {name} failed.") { }

        /// <inheritdoc/>
        protected AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
