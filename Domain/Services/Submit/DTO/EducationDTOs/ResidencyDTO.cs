namespace cred_system_back_end_app.Domain.Services.Submit.DTO.EducationDTOs
{
    public class ResidencyDTO : ListDTOBase
    {
        public AddressDTO AddressInfo { get; set; }
        public string InstitutionName { get; set; }
        public PeriodDTO Attendance { get; set; }
        public string ProgramType { get; set; }
        public string CompletionDate { get; set; }
        public FileBaseDTO EvidenceFile { get; set; }
        public string PostGraduateCompletionDate { get; set; }
    }
}
