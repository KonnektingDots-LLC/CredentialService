using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class MedicalGroupEntity : EntityCommon
    {
        public int Id { get; set; }

        public int MedicalGroupTypeId { get; set; }

        /// <summary>
        /// PcpOrSpecialistId
        /// </summary>
        public int CareTypeId { get; set; }

        public int? SpecifyPrimaryCareId { get; set; }

        public int? TypeOfSpecialistId { get; set; }

        public string? Name { get; set; }

        public string? MedicaidId { get; set; }

        public string? NPI { get; set; }

        public string? BillingNPI { get; set; }

        public string? TaxId { get; set; }

        public DateTime? EndorsementLetterDate { get; set; }

        public string? EmployerId_EIN { get; set; }

        public string? ContactPhone { get; set; }

        public string? EmailAddress { get; set; }

        #region related entities

        public List<ProviderEntity> Provider { get; } = new();

        public CareTypeEntity CareType { get; set; }

        public MedicalGroupTypeEntity MedicalGroupType { get; set; }

        public List<AddressEntity> Address { get; set; }

        public ICollection<MedicalGroupAddressEntity> MedicalGroupAddresses { get; set; }

        #endregion
    }
}
