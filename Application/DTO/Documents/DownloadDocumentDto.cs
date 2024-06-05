namespace cred_system_back_end_app.Application.DTO.Documents
{
    public class DownloadDocumentDto
    {
        public Stream Document { get; set; }
        public string ContentType { get; set; }
    }
}
