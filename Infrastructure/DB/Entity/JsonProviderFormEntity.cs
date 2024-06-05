namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class JsonProviderFormEntity
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsCompleted {get; set; }
        public string JsonBody { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
