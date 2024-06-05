namespace cred_system_back_end_app.Domain.Services.Submit.DTO
{
    public class AddressAndLocationDTO : ListDTOBase
    {
        public int AddressPrincipalTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsAcceptingNewPatient { get; set; }
        public bool IsComplyWithAda { get; set; }
        public string MedicalId { get; set; }
        public string? AdaCompliantComment { get; set; }
        public bool IsPhysicalAddressSameAsMail { get; set; }
        public AddressInfoDTO AddressInfo { get; set; }
        public DailyServiceHoursDTO[]? ServiceHours { get; set; }
    }
}
