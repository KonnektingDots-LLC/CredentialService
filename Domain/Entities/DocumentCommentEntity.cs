namespace cred_system_back_end_app.Domain.Entities
{
    public class DocumentCommentEntity
    {
        public int Id { get; set; }

        public string Comment { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; } = true;

        /* EF Relation */
        public int DocumentLocationId { get; set; }
        public DocumentLocationEntity DocumentLocation { get; set; }

        //DocumentCommentType --> DocumentComment
        public int DocumentCommentTypeId { get; set; }
        public DocumentCommentTypeEntity DocumentCommentType { get; set; }
    }
}
