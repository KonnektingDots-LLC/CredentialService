namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class DocumentSectionTypeEntity:RecordHistoryTypeList
    {
        public int Id { get; set; }
        public string ParentName { get; set; }
        public string Name { get; set; }

        public ICollection<DocumentTypeEntity> DocumentType { get; set; }
    }
}
