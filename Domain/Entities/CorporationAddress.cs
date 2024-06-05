using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class CorporationAddressEntity : EntityCommon
    {
        public int CorporationId { get; set; }

        public int AddressId { get; set; }

        public CorporationEntity Corporation { get; set; }

        public AddressEntity Address { get; set; }
    }
}
