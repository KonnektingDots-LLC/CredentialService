namespace cred_system_back_end_app.Application.UseCase.Insurer.DTO.CreateInsurerEmployee
{
    public class CreateInsurerEmployeeRequestDto
    {
        public string InsurerCompanyId { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }

        public string SurName { get; set; }
        public string Email { get; set; }
    }
}
