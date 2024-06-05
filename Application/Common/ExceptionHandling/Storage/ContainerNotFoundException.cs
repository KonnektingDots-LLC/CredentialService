using System.Runtime.Serialization;


namespace cred_system_back_end_app.Application.Common.ExceptionHandling.Storage
{
    /// <summary>
    /// An exception to be thrown by implementations of <see
    /// cref="Services.Storage.IStorageService"/> whenever an operation is requested against a file
    /// that does not exists.
    /// </summary>
    [Serializable]
    public class ContainerNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ContainerNotFoundException"/> class.
        /// </summary>
        /// <param name="containerName">The name of the container of the file.</param>
        public ContainerNotFoundException(string containerName)
            : base($"Container '{containerName}' does not exists.") { }

        /// <summary>
        /// Serialization constructor.
        /// </summary>
        /// <param name="info">Data storage for serialization data.</param>
        /// <param name="context">Describes the source and destination stream.</param>
        protected ContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
