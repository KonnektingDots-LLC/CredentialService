namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class NotificationProfileCompletionDetailEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool Sent { get; set; }
    }
}
