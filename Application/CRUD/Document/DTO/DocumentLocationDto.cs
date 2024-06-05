using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Application.CRUD.Document.DTO
{
    public class DocumentLocationDto
    {
        public string BlobFilename { get; set; }
        public int DocumentType { get; set; }
        public string UploadFilename { get; set; }
        public string Extension { get; set; }
        public bool IsUploaded { get; set; }
        public string? UploadBy { get; set; }

        public DateTime? UploadDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; } = true;

        public int ProviderId { get; set; }
    }
}
