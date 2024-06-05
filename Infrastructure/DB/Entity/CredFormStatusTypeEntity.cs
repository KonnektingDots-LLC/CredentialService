namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class CredFormStatusTypeEntity:RecordDate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string StateTypeId { get; set; }
        public int? PrioritySorting { get; set; }

        #region relationships

        public ICollection<CredFormEntity> CredForms { get; set; }
        public StateTypeEntity StateType { get; set; }

        #endregion
    }
}
