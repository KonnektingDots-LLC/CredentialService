using System.Runtime.Serialization;

namespace cred_system_back_end_app.Application.Common.ExceptionHandling.Storage
{
    /// <summary>
    /// An exception to be thrown by implementations of <see
    /// cref="Services.Storage.IStorageService"/> whenever an operation is requested against a file
    /// that does not exists.
    /// </summary>
    [Serializable]
    public class FileOrFolderNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="FileOrFolderNotFoundException"/> class.
        /// </summary>
        /// <param name="containerName">The name of the container of the file.</param>
        /// <param name="relativePath">
        /// The relative path of a file, including parent directories and excluding the container name.
        /// </param>
        /// <param name="inner">The inner exception that caused this exception.</param>
        public FileOrFolderNotFoundException(string containerName, string relativePath, Exception inner)
            : base($"File or folder '{containerName}/{relativePath}' does not exists.", inner) { }

        /// <summary>
        /// Serialization constructor.
        /// </summary>
        /// <param name="info">Data storage for serialization data.</param>
        /// <param name="context">Describes the source and destination stream.</param>
        protected FileOrFolderNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
