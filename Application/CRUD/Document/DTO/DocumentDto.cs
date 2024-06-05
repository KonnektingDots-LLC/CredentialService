using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Application.CRUD.Document.DTO
{
    public class DocumentDto
    {
        public string? Uri { get; set; }
        public string? FileName { get; set; }
        public string? DocumentType { get; set; }
        public Stream? Document { get; set; }
    }
}
