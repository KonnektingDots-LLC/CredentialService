namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class AddressCountryEntity:RecordHistoryTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iso { get; set; }
        public string? Iso3 { get; set; }

        public ICollection<AddressEntity> Address { get; set; }

    }
}
