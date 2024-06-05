using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class AddressEntity : RecordHistory
    {
        public int Id { get; set; }     
        public int AddressTypeId { get; set; }
        public AddressTypeEntity AddressType { get; set; }

        public string? Name { get; set; }
        
        public string Address1 { get; set; }
        
        public string? Address2 { get; set; }
        
        public string City { get; set; }
        
        public int AddressStateId { get; set; }
        public AddressStateEntity AddressState { get; set; }

        [MaxLength(60)]
        public string? StateOther { get; set; }
        
        public string? ZipCode { get; set; }

        public string? ZipCodeExtension { get; set; }

        public string? InternationalCode { get; set; }

        public bool IsClosed { get; set; }

        public int AddressCountryId { get; set; }
        public AddressCountryEntity AddressCountry { get; set; }

        #region 
        public ICollection<AddressServiceHourEntity> AddressServiceHours { get; set; }

        public List<CorporationEntity> Corporation { get; } = new();

        public List<MedicalGroupEntity> MedicalGroup { get; } = new();

        public List<ProviderEntity> Provider { get; } = new();

        #endregion

    }
}
