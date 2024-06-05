namespace cred_system_back_end_app.Application.UseCase.Insurer.DTO.RegisterProvider
{
    public class UpdateInsurerAdminRequestDTO : UpdateAdminBaseDTO
    {
    
    }

    public class UpdateOCSAdminRequestDTO : UpdateAdminBaseDTO
    {
    }    
    
    public class UpdateAdminBaseDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string?  MiddleName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
