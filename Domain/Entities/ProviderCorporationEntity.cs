using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class ProviderCorporationEntity : ListMemberEntityBase
    {
        public int ProviderId { get; set; }
        public int CorporationId { get; set; }

        public CorporationEntity Corporation { get; set; }
    }
}
