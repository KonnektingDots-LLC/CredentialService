namespace cred_system_back_end_app.Infrastructure.FileSystem.GetDocument.DTO
{
    public class DocumentReviewDto
    {
        public int DocumentTypeId { get; set; }
        public string UploadFilename { get; set; }
        public int ProviderId { get; set; }
    }
}
