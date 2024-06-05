using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class AddressCountryEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iso { get; set; }
        public string? Iso3 { get; set; }

        public ICollection<AddressEntity> Address { get; set; }

    }
}
