namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ProviderCorporationEntity : ListMemberEntityBase
    {
        public int ProviderId { get; set; }
        public int CorporationId { get; set; }

        public CorporationEntity Corporation { get; set; }
    }
}
