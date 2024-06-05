namespace cred_system_back_end_app.Application.DTO
{
    public class CreateDelegateDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public int DelegateTypeId { get; set; }
        public int DelegateCompanyId { get; set; }

    }
}
