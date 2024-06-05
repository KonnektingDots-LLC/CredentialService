namespace cred_system_back_end_app.Application.UseCase.Delegate.DTO
{
    public class DelegateCompanyDto
    {
        public string Name { get; set; }
        public string? RepresentativeFullName { get; set; }
        public string? RepresentativeEmail { get; set; }
        public bool IsActive { get; set; }
    }
}
