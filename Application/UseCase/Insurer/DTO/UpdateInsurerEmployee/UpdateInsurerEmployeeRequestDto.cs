namespace cred_system_back_end_app.Application.UseCase.Insurer.DTO.UpdateInsurerEmployee
{
    public class UpdateInsurerEmployeeRequestDto
    {
        public string Name { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        public string? SurName { get; set; }
    }
}
