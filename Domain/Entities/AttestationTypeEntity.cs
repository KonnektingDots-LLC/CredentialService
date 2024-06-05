using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class AttestationTypeEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AttestationEntity> Attestation { get; set; }
    }
}
