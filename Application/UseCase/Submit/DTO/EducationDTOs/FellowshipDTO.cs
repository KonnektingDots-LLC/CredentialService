namespace cred_system_back_end_app.Application.UseCase.Submit.DTO.EducationDTOs
{
    public class FellowshipDTO : ListDTOBase
    {
        public AddressDTO AddressInfo { get; set; }
        public string InstitutionName { get; set; }
        public PeriodDTO Attendance { get; set; }
        public string ProgramType { get; set; }
        public string CompletionDate { get; set; }
        public FileBaseDTO? EvidenceFile { get; set; }
    }
}
