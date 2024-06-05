namespace cred_system_back_end_app.Application.DTO.Requests
{
    public class CreateAccountDto
    {

        public CredsDTO Creds { get; set; }
        public PersonDTO Persona { get; set; }

    }

    public class CredsDTO
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string PassConfirm { get; set; }
    }

    public class MultiNPIDTO
    {
        public string Npi { get; set; }
        public string NpiCorporateName { get; set; }
    }

    public class PersonDTO
    {
        public List<MultiNPIDTO> MultiNPI { get; set; }
        public string UserRole { get; set; }
        public bool SameAsRenderingNPI { get; set; }
        public bool Attestation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string DOBMonth { get; set; }
        public string DOBDay { get; set; }
        public string DOBYear { get; set; }
        public string Gender { get; set; }
        public string RenderingNPI { get; set; }
        public string BillingNPI { get; set; }

        public int DelegateId { get; set; }
    }
}
