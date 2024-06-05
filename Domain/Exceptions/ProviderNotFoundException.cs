namespace cred_system_back_end_app.Domain.Exceptions
{
    public class ProviderNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ProviderNotFoundException"/> class.
        /// </summary>
        public ProviderNotFoundException()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ProviderNotFoundException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        public ProviderNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ProviderNotFoundException"/> class.
        /// </summary>
        /// <param name="message">A brief description about the exception.</param>
        /// <param name="inner">The original exception that caused this one.</param>
        public ProviderNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
