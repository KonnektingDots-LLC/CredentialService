namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class InsurerStatusTypeEntity : RecordDate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string StateTypeId { get; set; }
        public int? PrioritySorting { get; set; }

        #region relationships

        public StateTypeEntity StateType { get; set; }
        public ICollection<ProviderInsurerCompanyStatusEntity> ProviderInsurerCompanySatus { get; set; }
        public ICollection<ProviderInsurerCompanyStatusHistoryEntity> ProviderInsurerCompanySatusHistory { get; set; }

        #endregion
    }
}
