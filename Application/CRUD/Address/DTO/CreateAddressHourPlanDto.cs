using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Application.CRUD.Address.DTO
{
    public class CreateAddressHourPlanDto
    {
        [Required]
        public int AddressPlanAcceptId { get; set; }
        [Required]
        public int AddressServiceHourId { get; set; }
    }
}
