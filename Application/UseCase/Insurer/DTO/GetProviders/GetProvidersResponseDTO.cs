namespace cred_system_back_end_app.Application.UseCase.Insurer.DTO.GetProviders
{
    public class GetProvidersResponseDTO
    {
        public ProviderInfo[] Providers { get; set; }
    }

    public class ProviderInfo 
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string SecondLastName { get; set; }
    }
}
