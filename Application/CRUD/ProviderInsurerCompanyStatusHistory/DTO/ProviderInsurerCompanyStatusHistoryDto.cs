namespace cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatusHistory.DTO
{
    public class ProviderInsurerCompanyStatusHistoryDto
    {
        public int ProviderInsurerCompanyStatusId { get; set; }
        public string InsurerStatusTypeId {  get; set; }
        public DateTime StatusDate { get; set; }
        public string? Comment { get; set; }
        public DateTime? CommentDate { get; set; }
        public string CreatedBy { get; set; }

    }
}
