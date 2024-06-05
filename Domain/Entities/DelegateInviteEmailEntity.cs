namespace cred_system_back_end_app.Domain.Entities
{
    public class DelegateInviteEmailEntity
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string Email { get; set; }
    }
}
