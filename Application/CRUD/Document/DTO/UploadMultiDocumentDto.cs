namespace cred_system_back_end_app.Application.CRUD.Document.DTO
{
    public class UploadMultiDocumentDto
    {
        public IFormFile File { get; set; }
        public int ProviderId { get; set; }
        public string DocumentCode { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string UploadBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
