namespace cred_system_back_end_app.Application.DTO.Requests
{
    public class CreateProviderByDelegateDto
    {
        public CredsDTO Creds { get; set; }
        public PersonDTO Persona { get; set; }

        // public CreateDelegateDto Delegate { get; set; }
    }

}
