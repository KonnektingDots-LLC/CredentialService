namespace cred_system_back_end_app.Application.UseCase.Submit.DTO.EducationDTOs
{
    public class EducationAndTrainingDTO
    {
        public List<MedicalSchoolDTO> MedicalSchool { get; set; }
        public List<InternshipDTO>? Internship { get; set; }
        public List<ResidencyDTO?>? Residency { get; set; }
        public List<FellowshipDTO?>? Fellowship { get; set; }
        public List<BoardCertificateDTO>? BoardCertificates { get; set; }
        public LicensesCertificatesDTO LicensesCertificates { get; set; }
    }
}
