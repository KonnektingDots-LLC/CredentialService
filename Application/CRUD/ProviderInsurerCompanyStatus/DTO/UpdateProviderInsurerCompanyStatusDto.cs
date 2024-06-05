namespace cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatus.DTO
{
    public class UpdateProviderInsurerCompanyStatusDto
    {
        public int Id { get; set; }
        public string InsurerStatusTypeId { get; set; }
        public DateTime CurrentStatusDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public string? Comment { get; set; }
        public DateTime CommentDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
