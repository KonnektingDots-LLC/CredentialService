namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ChangeLogResourceTypeEntity : RecordDate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        #region Relationships
        public ICollection<ChangeLogEntity> ChangeLog { get; set; }
        #endregion

    }
}
