using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class DocumentSectionTypeEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }
        public string ParentName { get; set; }
        public string Name { get; set; }

        public ICollection<DocumentTypeEntity> DocumentType { get; set; }
    }
}
