namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ProviderInsurerCompanyStatusHistoryEntity : RecordDate
    {
        public int Id { get; set; }
        public int ProviderInsurerCompanyStatusId { get; set; }
        public string InsurerStatusTypeId { get; set; }
        public DateTime StatusDate { get; set; }
        public string? Comment { get; set; }
        public DateTime? CommentDate { get; set; }

        #region relationships

        public ProviderInsurerCompanyStatusEntity ProviderInsurerCompanyStatus { get; set; }
        public InsurerStatusTypeEntity InsurerStatusType { get; set; }

        #endregion

    }
}
