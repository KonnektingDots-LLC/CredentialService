using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class AddressPrincipalTypeEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProviderAddressEntity> ProviderAddresses { get; set; }
    }
}
