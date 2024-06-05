using System.Reflection.Metadata.Ecma335;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class CorporationEntity : RecordHistory
    {
        public int Id { get; set; }     
        public int CorpTaxIdTypeId { get; set; }
        public bool ParticipateMedicaid { get; set; }
        public string? MedicaidIdLocation { get; set; }
        public int SpecialtyType { get; set; }
        public string CorporatePracticeName { get; set; }
        public DateTime IncorporationEffectiveDate { get; set; }
        public string BillingNPI{ get; set; }
        public string RenderingNPI { get; set; }
        public string CorpTaxIdNumber { get; set; }
        public string? ContactPhoneNumber { get; set; }
        public int EntityTypeId { get; set; }  

        #region relationships
        public EntityTypeEntity EntityType { get; set; }
        public CorpTaxIdTypeEntity CorpTaxIdType { get; set; }
        public List<ProviderEntity> Provider { get; } = new();
        public List<SubSpecialtyListEntity> SubSpecialty { get; } = new();
        public List<AddressEntity> Address { get; set; } = new();
        public ICollection<CorporationAddressEntity> CorporationAddresses { get; set; }
        public ICollection<CorporationDocumentEntity> CorporationDocument { get; set;}
        #endregion
    }
}
