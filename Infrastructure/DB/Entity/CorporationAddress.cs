namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class CorporationAddressEntity : RecordHistory
    {
        public int CorporationId { get; set; }

        public int AddressId { get; set; }

        public CorporationEntity Corporation { get; set; }

        public AddressEntity Address { get; set; }
    }
}
