namespace cred_system_back_end_app.Domain.Services.Submit.DTO.EducationDTOs
{
    public class MedicalSchoolDTO
    {
        public string PublicId { get; set; }
        public string SchoolName { get; set; }
        public AddressDTO AddressInfo { get; set; }
        public int GraduationMonth { get; set; }
        public int GraduationYear { get; set; }
        public string Specialty { get; set; }
        public string SpecialtyCompletionDate { get; set; }
        public string SpecialtyDegree { get; set; }
        public FileBaseDTO DiplomaFile { get; set; }
    }
}
