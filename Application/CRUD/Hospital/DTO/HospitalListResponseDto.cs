using cred_system_back_end_app.Application.Common.ResponseDTO;
using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Application.CRUD.Hospital.DTO
{
    public class HospitalListResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BusinessName { get; set; }
    }
}
