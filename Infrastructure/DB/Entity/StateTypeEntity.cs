namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class StateTypeEntity : RecordDate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        #region relationships

        public ICollection<CredFormStatusTypeEntity> CredFormStatusTypes { get; set; }
        public ICollection<InsurerStatusTypeEntity> InsurerStatusTypes { get; set; }

        #endregion
    }
}
