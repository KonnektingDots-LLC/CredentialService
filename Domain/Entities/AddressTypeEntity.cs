using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class AddressTypeEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AddressEntity> Address { get; set; }
    }
}
