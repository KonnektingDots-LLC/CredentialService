namespace cred_system_back_end_app.Application.DTO.Documents
{
    public class PdfUploadDto
    {
        public int ProviderId { get; set; }
        public string UploadBy { get; set; }
        public string? ModifiedBy { get; set; }
        public Stream PdfStream { get; set; }
        public string UploadFileName { get; set; }
        public int DocumentTypeId { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
