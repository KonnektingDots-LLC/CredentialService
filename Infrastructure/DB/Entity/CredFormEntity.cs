namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class CredFormEntity : RecordDate
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Version { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string CredFormStatusTypeId { get; set; }
        public DateTime? SubmitDate { get; set; }
        public DateTime? ReSubmitDate { get; set; }

        #region relationships
        public CredFormStatusTypeEntity CredFormStatusType { get; set;}
        public ProviderEntity Provider { get; set; }
        #endregion

    }
}
