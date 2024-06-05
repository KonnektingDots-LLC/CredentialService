namespace cred_system_back_end_app.Application.CRUD.Provider.DTO
{
    public class ProviderCredFormResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string CredFormId { get; set; }
        public int Version { get; set; }
        public string ProviderStatus { get; set; }
    }
}
