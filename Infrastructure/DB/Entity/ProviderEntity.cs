using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ProviderEntity:RecordHistory
    {
        public int Id { get; set; }
        /* BEGIN EF Relation */
        public int ProviderTypeId { get; set; }
        public ProviderTypeEntity ProviderType { get; set; }

        /* END EF Relation */
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
       
        public string Email { get; set; }
        [MaxLength(10), MinLength(10)]
        public string? PhoneNumber { get; set; }

        public bool SameAsRenderingNPI { get; set; }

        public int CredFormId { get; set; }
        /* EF Relation */

        #region relationships
        public ProviderDetailEntity ProviderDetail { get; set; }

        public List<CorporationEntity> Corporation { get; } = new();

        public List<MedicalGroupEntity> MedicalGroup { get; } 

        public List<MultipleNPIEntity> MultipleNPI { get; set; }

        public List<HospitalEntity> Hospital { get; } = new();
        
        public ICollection<MalpracticeEntity> Malpractice { get; set; }

        public ICollection<ProfessionalLiabilityEntity> ProfessionalLiability { get; set; }

        public ICollection<MedicalLicenseEntity> MedicalLicenses { get; set; }

        public List<EducationInfoEntity> EducationInfo { get; } = new();

        public ICollection<BoardEntity> Board { get; set; }

        public ICollection<ProviderPlanAcceptEntity> ProviderPlanAccept { get; set; }

        public ICollection<ProviderSpecialtyEntity> ProviderSpecialty { get; set; }

        public ICollection<ProviderSubSpecialtyEntity> ProviderSubSpecialty { get; set; }

        public ICollection<ProviderDelegateEntity> ProviderDelegate { get; set; }

        public List<AddressEntity> Address { get; set; } = new();

        public ICollection<MedicalSchoolEntity> MedicalSchools { get; set; }

        public ICollection<DocumentLocationEntity> DocumentLocation { get; set; }

        public CredFormEntity CredForm { get; set; }

        public ICollection<ProviderInsurerCompanyStatusEntity> ProviderInsurerCompanyStatus { get; set; }

        #endregion

    }
}
