using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Domain.Services.Submit.DTO
{
    public class AddressInfoDTO
    {
        public bool IsPhysicalAddressSameAsMail { get; set; }
        public AddressDTO Physical { get; set; }
        public AddressDTO Mail { get; set; }
    }

    public class AddressDTO
    {
        public string? Name { get; set; }

        [MaxLength(60), MinLength(1)]
        public string Address1 { get; set; }

        [MaxLength(60)]
        public string? Address2 { get; set; }

        [MaxLength(60), MinLength(1)]
        public string City { get; set; }

        public int StateId { get; set; }
        public int AddressCountryId { get; set; }
        public string? StateOther { get; set; }

        [MaxLength(5)]
        public string? ZipCode { get; set; }

        [MaxLength(4)]
        public string? ZipCodeExtension { get; set; }

        [MaxLength(10)]
        public string? InternationalCode { get; set; }
    }
}
