namespace cred_system_back_end_app.Domain.Exceptions
{
    [Serializable]
    internal class DocumentNotFoundException : Exception
    {
        public DocumentNotFoundException()
        {
        }

        public DocumentNotFoundException(string? message) : base(message)
        { }

        public DocumentNotFoundException(int providerId, int documentTypeId, string? filename) : base($"Document was not found by providerId " +
                    $"[{providerId}] newDocumentLocation [{documentTypeId}] and UploadFilename [{filename}]")
        {
        }

        public DocumentNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}