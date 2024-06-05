namespace cred_system_back_end_app.Domain.Entities
{
    public class DocumentCommentTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; } = true;

        /* EF Relation */
        public List<DocumentCommentEntity> DocumentComments { get; set; }

    }
}
