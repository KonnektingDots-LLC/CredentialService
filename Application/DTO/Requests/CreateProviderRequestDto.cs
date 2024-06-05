using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Application.DTO.Requests
{
    public class CreateProviderRequestDto
    {
        public int ProviderTypeId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string? SurName { get; set; }

        public DateTime BirthDate { get; set; }
        [MaxLength(20)]
        public string Gender { get; set; }
        [MaxLength(10), MinLength(10)]
        public string RenderingNPI { get; set; }
        [MaxLength(10), MinLength(10)]
        public string BillingNPI { get; set; }

        public bool SameAsRenderingNPI { get; set; }

        public string Email { get; set; }
        [MaxLength(10), MinLength(10)]
        public string PhoneNumber { get; set; }

        public List<MultipleNPIDTO> MultipleNPI { get; set; }

        //public int CredFormId { get; set; }


    }
}
