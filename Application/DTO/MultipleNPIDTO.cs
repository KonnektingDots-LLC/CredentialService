using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Application.DTO
{
    public class MultipleNPIDTO
    {
        [MaxLength(14)]
        public string NPI { get; set; }
        [MaxLength(50)]
        public string CorporateName { get; set; }
    }
}
