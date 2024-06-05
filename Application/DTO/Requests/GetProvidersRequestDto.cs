namespace cred_system_back_end_app.Application.DTO.Requests
{
    public class GetProvidersRequestDto
    {
        public string? UserEmail { get; set; }
        public string? UserRole { get; set; }
        public string? Search { get; set; }
        public int CurrentPage { get; set; }
        public int LimitPerPage { get; set; }
        public string? Email { get; set; }
        public bool dofilterByRole { get; set; }
    }
}
