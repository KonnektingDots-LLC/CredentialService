namespace cred_system_back_end_app.Domain.Services.Submit.DTO
{
    public class CriminalRecordDTO
    {
        //TODO: create this column in table ProviderDetail
        public string NegativePenalRecordIssuedDate { get; set; }
        public string NegativePenalRecordExpDate { get; set; }
        public FileBaseDTO NegativePenalRecordFile { get; set; }
    }
}
