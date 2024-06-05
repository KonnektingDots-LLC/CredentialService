namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class AddressStateEntity:RecordHistoryTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AddressEntity> Address { get; set; }
    }
}
