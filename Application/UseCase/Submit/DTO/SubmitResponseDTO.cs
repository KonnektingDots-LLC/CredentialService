namespace cred_system_back_end_app.Application.UseCase.Submit.DTO
{
    public class SubmitResponseDTO
    {
        public int ProviderId { get; set; }
        public string[] EducationAndTraining { get; set; }
        public string[] HospitalAffiliations { get; set; }
        public string IncorporatedPractice { get; set; }
        public string[] MedicalGroups { get; set; }
        public string[] LicensesAndCertificates { get; set; }
    }
}
