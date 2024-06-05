using AutoMapper.Configuration.Conventions;
using cred_system_back_end_app.Application.Common.ResponseDTO;

namespace cred_system_back_end_app.Application.UseCase.Delegate.DTO
{
    public class DelegateListResponseDTO : PaginatedResponseBaseDTO<DelegateInfoDto>
    {

    }

    public class DelegateInfoDto 
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
    }
}
