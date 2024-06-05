namespace cred_system_back_end_app.Application.UseCase.Submit.DTO
{
    public class SpecialtiesAndSubspecialtiesDTO
    {
        public List<SpecialtyDTO> Specialties { get; set; }
        public List<SpecialtyDTO>? Subspecialties { get; set; }
    }

    public class SpecialtyDTO
    {
        public int Id { get; set; }
        public FileBaseDTO EvidenceFile { get; set; }
    }
}
