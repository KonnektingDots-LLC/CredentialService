namespace cred_system_back_end_app.Infrastructure.FileSystem.GetDocument.DTO
{
    public class DownloadDocumentsDto
    {
        public MemoryStream Documents { get; set; }
        public string ContentType { get; set; }
        public string ZipFilename { get; set; }
    }
}
