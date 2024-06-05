namespace cred_system_back_end_app.Domain.Services.DTO
{
    public class MultiFileUploadDto
    {
        public IFormFile File { get; set; }
        public int DocumentTypeId { get; set; }
        public string? NPI { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? LetterDate { get; set; }
        public DateTime? CertificateDate { get; set; }
        public string? OldFilename { get; set; }
    }
}
