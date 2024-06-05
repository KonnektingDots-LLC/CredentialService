namespace cred_system_back_end_app.Application.DTO.Responses
{
    public class PaginatedResponseBaseDto<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int LimitPerPage { get; set; }
        public int TotalNumberOfPages { get; set; }
        public IEnumerable<T>? Content { get; set; }
    }
}
