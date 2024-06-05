namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ProviderInsurerCompanyStatusEntity : RecordDate
    {
        public int Id { get; set; }
        public string InsurerStatusTypeId { get; set; }
        public int ProviderId { get; set; }
        public string InsurerCompanyId { get; set; }
        public DateTime CurrentStatusDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public string? Comment { get; set; }
        public DateTime? CommentDate { get; set; }

        #region relationships

        public InsurerStatusTypeEntity InsurerStatusType { get; set; }
        public ProviderEntity Provider { get; set; }
        public InsurerCompanyEntity InsurerCompany { get; set; }
        public ICollection<ProviderInsurerCompanyStatusHistoryEntity> ProviderInsurerCompanySatusHistory { get; set; }

        #endregion
    }
}
