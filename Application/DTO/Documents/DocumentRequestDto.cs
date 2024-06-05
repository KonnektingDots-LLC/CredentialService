namespace cred_system_back_end_app.Application.DTO.Documents
{
    public class DocumentRequestDto
    {
        public int DocumentTypeId { get; set; }
        public List<string>? Filename { get; set; }
        public bool IsAzureBlobFilename { get; set; }
        public string? Format { get; set; } //PDF or ZIP
        public bool DownloadAll { get; set; }
    }
}
