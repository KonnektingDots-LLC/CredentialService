using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Domain.Entities
{
    public class AddressHourPlanEntity
    {
        public int Id { get; set; }
        [Required]
        public int AddressPlanAcceptId { get; set; }
        [Required]
        public int AddressServiceHourId { get; set; }
    }
}
