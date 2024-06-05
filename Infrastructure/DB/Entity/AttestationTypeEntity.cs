namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class AttestationTypeEntity:RecordHistoryTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AttestationEntity> Attestation { get; set; }
    }
}
