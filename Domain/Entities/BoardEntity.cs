using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class BoardEntity : ListMemberEntityBase
    {
        public int Id { get; set; }

        public int ProviderId { get; set; }

        public DateTime? SBCertificateIssuedDate { get; set; }

        public DateTime? SBCertificateExpirationDate { get; set; }

        public bool EvidenceSubmitted { get; set; }

        #region related entities

        public ProviderEntity Provider { get; set; }

        public List<SpecialtyListEntity> Specialty { get; } = new();

        public BoardDocumentEntity BoardDocument { get; set; }

        #endregion
    }
}
