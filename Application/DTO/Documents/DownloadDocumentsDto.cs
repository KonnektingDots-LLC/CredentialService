namespace cred_system_back_end_app.Application.DTO.Documents
{
    public class DownloadDocumentsDto
    {
        public MemoryStream Documents { get; set; }
        public string ContentType { get; set; }
        public string ZipFilename { get; set; }
    }
}
