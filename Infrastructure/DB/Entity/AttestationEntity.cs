namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class AttestationEntity:RecordHistory
    {
        public int Id { get; set; }
        public bool IsAccept { get; set; }
        public int ProviderId { get; set; }
        public DateTime AttestationDate { get; set; }
        public int AttestationTypeId { get; set; }

        #region relationships
        public AttestationTypeEntity AttestationType { get; set; }
        public ProviderEntity Provider { get; set; }
        #endregion
    }
}
