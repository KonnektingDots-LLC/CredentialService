namespace cred_system_back_end_app.Application.DTO
{
    public class DelegateTypeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsExpired { get; set; }
        public DateTime? ExpiredDate { get; set; }

    }
}
