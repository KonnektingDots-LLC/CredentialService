namespace cred_system_back_end_app.Domain.Entities
{
    public class ChangeLogEntity
    {
        public int Id { get; set; }
        public string ChangeLogResourceTypeId { get; set; }
        public string? ChangeLogResourceId { get; set; } //Not Reliontionship
        public string ChangeLogUseCaseTypeId { get; set; }
        public string? ChangeLogUseCaseTargetId { get; set; } //Not Reliontionship
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime ChangedDate { get; set; }
        public string ChangedBy { get; set; }

        #region Relationships
        public ChangeLogResourceTypeEntity ChangeLogResourceType { get; set; }
        public ChangeLogUserCaseTypeEntity ChangeLogUseCaseType { get; set; }
        #endregion
    }
}
