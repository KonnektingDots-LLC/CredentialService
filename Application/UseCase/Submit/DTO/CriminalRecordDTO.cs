namespace cred_system_back_end_app.Application.UseCase.Submit.DTO
{
    public class CriminalRecordDTO
    {
        //TODO: create this column in table ProviderDetail
        public string NegativePenalRecordIssuedDate { get; set; }
        public string NegativePenalRecordExpDate { get; set; }
        public FileBaseDTO NegativePenalRecordFile { get; set; }
    }
}
