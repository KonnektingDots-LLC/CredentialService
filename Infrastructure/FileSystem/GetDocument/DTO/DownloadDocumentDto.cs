namespace cred_system_back_end_app.Infrastructure.FileSystem.GetDocument.DTO
{
    public class DownloadDocumentDto
    {
        public Stream Document { get; set; }
        public string ContentType { get; set; }
    }
}
