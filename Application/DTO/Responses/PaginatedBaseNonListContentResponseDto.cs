namespace cred_system_back_end_app.Application.DTO.Responses
{
    public class PaginatedBaseNonListContentResponseDto<T>
    {
        public int CurrentPage { get; set; }
        public int LimitPerPage { get; set; }
        public int TotalNumberOfPages { get; set; }
        public T Content { get; set; }
    }
}
