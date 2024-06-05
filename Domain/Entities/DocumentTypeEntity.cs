using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class DocumentTypeEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }
        public int DocumentSectionTypeId { get; set; }
        public DocumentSectionTypeEntity DocumentSectionType { get; set; }
        public string Name { get; set; }

        /* EF Relations */
        public List<DocumentLocationEntity> DocumentLocation { get; set; }
    }
}
