namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class PaginatedProviderResponseDTO : PaginatedResponseBaseDTO<ProviderResponseBaseDTO>
    {

    }

    public class InsurerEmployeePaginatedResponse //: PaginatedResponseBaseDTO<InsurerEmployeeResponseDTO>
    {
        public int CurrentPage { get; set; }
        public int LimitPerPage { get; set; }
        public int TotalNumberOfPages { get; set; }
        public IEnumerable<InsurerEmployeeResponseDTO> Content { get; set; }
    }

    public class PaginatedResponseBaseDTO<T>
    {
        public int CurrentPage { get; set; }
        public int LimitPerPage { get; set; }
        public int TotalNumberOfPages { get; set; }
        public IEnumerable<T> Content { get; set; }
    }

    public class PaginatedResponseBaseDTONonListContent<T>
    {
        public int CurrentPage { get; set; }
        public int LimitPerPage { get; set; }
        public int TotalNumberOfPages { get; set; }
        public T Content { get; set; }
    }
}
