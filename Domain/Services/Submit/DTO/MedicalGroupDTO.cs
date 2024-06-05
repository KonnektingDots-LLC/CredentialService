namespace cred_system_back_end_app.Domain.Services.Submit.DTO
{
    public class MedicalGroupDTO
    {
        public AddressInfoDTO AddressInfo { get; set; }
        public string PmgName { get; set; }
        public string BillingNpiNumber { get; set; }
        public string TaxIdGroup { get; set; }
        public string MedicaidIdLocation { get; set; }
        public string NpiGroupNumber { get; set; }
        public string? EndorsementLetterDate { get; set; }
        public FileBaseDTO? EndorsementLetterFile { get; set; }
        public string EmployerId { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// PCP or Specialist Radio.
        /// PCP = 1 
        /// Specialist = 2
        /// This belongs in CareType table
        /// </summary>
        public int PcpOrSpecialistId { get; set; }

        /// <summary>
        /// Type of primary care
        /// If MedicalGroupTypeId = 1 -> This must be non-zero 
        /// This belongs to SpecialtyList table
        /// </summary>
        public int? SpecifyPrimaryCareId { get; set; } // 'Specify Primary Care'

        /// <summary>
        /// If MedicalGroupType = 2 -> This must be non-zero
        /// This belongs to SpecialtyList 
        /// </summary>
        public int? TypeOfSpecialistId { get; set; } //LLEON: el 0 significara Empty

        public DailyServiceHoursDTO[]? ServiceHours { get; set; }
    }
}
