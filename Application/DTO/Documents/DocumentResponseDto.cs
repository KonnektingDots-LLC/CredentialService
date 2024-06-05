namespace cred_system_back_end_app.Application.DTO.Documents
{
    public class DocumentResponseDto
    {
        public string? Status { get; set; }
        public bool Error { get; set; }

        public DocumentDto Document { get; set; }

        public DocumentResponseDto()
        {
            Document = new DocumentDto();
        }
    }

    public class PdfDocumentResponse
    {
        public string Filename { get; set; }
    }
}
