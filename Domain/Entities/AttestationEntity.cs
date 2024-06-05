using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class AttestationEntity : EntityCommon
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
