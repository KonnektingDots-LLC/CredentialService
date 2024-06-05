namespace cred_system_back_end_app.Application.CRUD.MedicalGroup.DTO
{
    public class MedicalGroupRequestDTO
    {
        public string PmgName { get; set; }
        public string BillingNpiNumber { get; set; }
        public string TaxIdGroup { get; set; }
        public string NpiGroupNumber { get; set; }
        public string MedicaidIdLocation { get; set; }
        public AddressInfo AddressInfo { get; set; }
        public string EndorsementLetterDate { get; set; }
        public string EndorsementLetter { get; set; }
        public string EmployerId { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public int CareTypeId { get; set; }
        public int CareOptionId { get; set; }
        public ServiceHours ServiceHours { get; set; }

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AddressInfo
    {
        public bool IsPhysicalAddressSameAsMail { get; set; }
        public Physical Physical { get; set; }
        public Mail Mail { get; set; }
    }

    public class Friday
    {
        public string HourFrom { get; set; }
        public string HourTo { get; set; }
        public bool IsClosed { get; set; }
    }

    public class Mail
    {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }
        public string ZipCode { get; set; }
        public string County { get; set; }
    }

    public class Monday
    {
        public string HourFrom { get; set; }
        public string HourTo { get; set; }
        public bool IsClosed { get; set; }
    }

    public class Physical
    {
        public string AddressName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }
        public string ZipCode { get; set; }
        public string County { get; set; }
    }

    public class Saturday
    {
        public string HourFrom { get; set; }
        public string HourTo { get; set; }
        public bool IsClosed { get; set; }
    }

    public class ServiceHours
    {
        public Monday Monday { get; set; }
        public Tuesday Tuesday { get; set; }
        public Wednesday Wednesday { get; set; }
        public Thursday Thursday { get; set; }
        public Friday Friday { get; set; }
        public Saturday Saturday { get; set; }
        public Sunday Sunday { get; set; }
    }

    public class Sunday
    {
        public string HourFrom { get; set; }
        public string HourTo { get; set; }
        public bool IsClosed { get; set; }
    }

    public class Thursday
    {
        public string HourFrom { get; set; }
        public string HourTo { get; set; }
        public bool IsClosed { get; set; }
    }

    public class Tuesday
    {
        public string HourFrom { get; set; }
        public string HourTo { get; set; }
        public bool IsClosed { get; set; }
    }

    public class Wednesday
    {
        public string HourFrom { get; set; }
        public string HourTo { get; set; }
        public bool IsClosed { get; set; }
    }


}
