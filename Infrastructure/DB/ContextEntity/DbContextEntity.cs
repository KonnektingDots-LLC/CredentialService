using cred_system_back_end_app.Application.CRUD.MedicalGroup.DTO;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace cred_system_back_end_app.Infrastructure.DB.ContextEntity
{
    public class DbContextEntity : DbContext
    {
        public DbContextEntity(DbContextOptions<DbContextEntity> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProviderEntity>()
            .HasIndex(a => a.Email)
            .IsUnique();

            modelBuilder.Entity<CorporationEntity>()
                .HasMany(corporation => corporation.Address)
                .WithMany(addresses => addresses.Corporation)
                .UsingEntity<CorporationAddressEntity>();              
            
            modelBuilder.Entity<CorporationEntity>()
                .HasMany(corporation => corporation.SubSpecialty)
                .WithMany(subspecialty => subspecialty.Corporation)
                .UsingEntity<CorporationSubSpecialtyEntity>();            
            
            modelBuilder.Entity<MedicalGroupEntity>()
                .HasMany(medicalGroup => medicalGroup.Address)
                .WithMany(addresses => addresses.MedicalGroup)
                .UsingEntity<MedicalGroupAddressEntity>();            
            
            modelBuilder.Entity<ProviderEntity>()
                .HasMany(provider => provider.Address)
                .WithMany(addresses => addresses.Provider)
                .UsingEntity<ProviderAddressEntity>();

            modelBuilder.Entity<ProviderEntity>()
                .HasMany(provider => provider.EducationInfo)
                .WithMany(education => education.Provider)
                .UsingEntity<ProviderEducationInfoEntity>();

            modelBuilder.Entity<ProviderEntity>()
                .HasMany(provider => provider.Corporation)
                .WithMany(corporation => corporation.Provider)
                .UsingEntity<ProviderCorporationEntity>();

            modelBuilder.Entity<ProviderEntity>()
                .HasMany(provider => provider.MedicalGroup)
                .WithMany(medicalGroup => medicalGroup.Provider)
                .UsingEntity<ProviderMedicalGroupEntity>();            
            
            modelBuilder.Entity<ProviderEntity>()
                .HasMany(provider => provider.Hospital)
                .WithMany(hospital => hospital.Provider)
                .UsingEntity<ProviderHospitalEntity>();

            modelBuilder.Entity<ProviderSpecialtyEntity>()
            .HasKey(ps => new { ps.ProviderId, ps.SpecialtyListId, ps.AzureBlobFileName });

            modelBuilder.Entity<ProviderSubSpecialtyEntity>()
            .HasKey(pss => new { pss.ProviderId, pss.SubSpecialtyListId, pss.DocumentLocationId });

            modelBuilder.Entity<CorporationDocumentEntity>()
            .HasKey(cd => new { cd.CorporationId,cd.DocumentLocationId });

            modelBuilder.Entity<MedicalSchoolDocumentEntity>()
            .HasKey(cd => new { cd.MedicalSchoolId, cd.AzureBlobFilename });

            modelBuilder.Entity<BoardDocumentEntity>()
            .HasKey(cd => new { cd.BoardId, cd.AzureBlobFilename });

            modelBuilder.Entity<EducationInfoDocumentEntity>()
            .HasKey(cd => new { cd.EducationInfoId, cd.AzureBlobFilename });

            modelBuilder.Entity<BoardEntity>()
            .HasMany(b => b.Specialty)
            .WithMany(s => s.Board)
            .UsingEntity<BoardSpecialtyEntity>();

            OneToOneRelationshipConfiguration(modelBuilder);
            OneToManyRelationshipConfiguration(modelBuilder);
        }


        private void OneToOneRelationshipConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProviderEntity>()
                .HasOne(p => p.ProviderDetail)
                .WithOne(pd => pd.Provider)
                .HasForeignKey<ProviderDetailEntity>(pd => pd.ProviderId);

            //modelBuilder.Entity<ProviderCorporationEntity>()
            //    .HasKey();

            modelBuilder.Entity<PeriodEntity>()
                .HasOne(p => p.Hospital)
                .WithOne(h => h.HospitalPriviledgePeriod)
                .HasForeignKey<HospitalEntity>(h => h.HospitalPriviledgePeriodId);

            modelBuilder.Entity<DocumentLocationEntity>()
                .HasOne(dl => dl.ProviderSpecialty)
                .WithOne(ps => ps.DocumentLocation)
                .HasForeignKey<ProviderSpecialtyEntity>(ps=> ps.AzureBlobFileName);

            modelBuilder.Entity<DocumentLocationEntity>()
            .HasOne(dl => dl.ProviderSubSpecialty)
            .WithOne(ps => ps.DocumentLocation)
            .HasForeignKey<ProviderSubSpecialtyEntity>(ps => ps.DocumentLocationId);

            modelBuilder.Entity<DocumentLocationEntity>()
            .HasOne(dl => dl.CorporationDocument)
            .WithOne(ps => ps.DocumentLocation)
            .HasForeignKey<CorporationDocumentEntity>(ps => ps.DocumentLocationId);

            modelBuilder.Entity<MalpracticeOIGCaseNumbers>()
                    .HasKey(m => new {m.OIGCaseNumber, m.MalpracticeId });

            modelBuilder.Entity<NotificationStatusEntity>()
            .HasOne(n => n.Notification)
            .WithOne(ns => ns.NotificationStatus)
            .HasForeignKey<NotificationEntity>(n => n.NotificationStatusId);

            modelBuilder.Entity<CredFormEntity>()
            .HasOne(cf => cf.Provider)
            .WithOne(p => p.CredForm)
            .HasForeignKey<ProviderEntity>(p => p.CredFormId);

            modelBuilder.Entity<BoardEntity>()
            .HasOne(dl => dl.BoardDocument)
            .WithOne(ps => ps.Board)
            .HasForeignKey<BoardDocumentEntity>(ps => ps.BoardId);

            modelBuilder.Entity<DocumentLocationEntity>()
            .HasOne(dl => dl.BoardDocument)
            .WithOne(ps => ps.DocumentLocation)
            .HasForeignKey<BoardDocumentEntity>(ps => ps.AzureBlobFilename);

            //EducationInfo
            modelBuilder.Entity<EducationInfoEntity>()
            .HasOne(dl => dl.EducationInfoDocument)
            .WithOne(ps => ps.EducationInfo)
            .HasForeignKey<EducationInfoDocumentEntity>(ps => ps.EducationInfoId);

            modelBuilder.Entity<DocumentLocationEntity>()
            .HasOne(dl => dl.EducationInfoDocument)
            .WithOne(ps => ps.DocumentLocation)
            .HasForeignKey<EducationInfoDocumentEntity>(ps => ps.AzureBlobFilename);

            //MedicalShool
            modelBuilder.Entity<MedicalSchoolEntity>()
            .HasOne(dl => dl.MedicalSchoolDocument)
            .WithOne(ps => ps.MedicalSchool)
            .HasForeignKey<MedicalSchoolDocumentEntity>(ps => ps.MedicalSchoolId);

            modelBuilder.Entity<DocumentLocationEntity>()
            .HasOne(dl => dl.MedicalSchoolDocument)
            .WithOne(ps => ps.DocumentLocation)
            .HasForeignKey<MedicalSchoolDocumentEntity>(ps => ps.AzureBlobFilename);
        }

        private void OneToManyRelationshipConfiguration(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ProviderSpecialtyEntity>()
            // .HasOne(dps => dps.DocumentLocation)
            // .WithMany(d => d.ProviderSpecialty)
            // .HasForeignKey(dps => dps.DocumentLocationId);

            //modelBuilder.Entity<ProviderSubSpecialtyEntity>()
            // .HasOne(dps => dps.DocumentLocation)
            // .WithMany(ps => ps.ProviderSubSpecialty)
            // .HasForeignKey(dps => dps.DocumentLocationId);

        }

        // Provider
        public DbSet<ProviderEntity> Provider { get; set; }
        //public DbSet<ProviderDelegateEntity> ProviderDelegate { get; set; }
        public DbSet<ProviderDetailEntity> ProviderDetail { get; set; }
        public DbSet<ProviderTypeEntity> ProviderType { get; set; }
        public DbSet<MultipleNPIEntity> MultipleNPI { get; set; }
        public DbSet<ProviderPlanAcceptEntity> ProviderPlanAccept { get; set; }
        public DbSet<PlanAcceptListEntity> PlanAcceptList { get; set; }
        public DbSet<ProviderHospitalEntity> ProviderHospital { get; set; }
        public DbSet<ProviderEducationInfoEntity> ProviderEducationInfo { get; set; }
        public DbSet<ProviderSpecialtyEntity> ProviderSpecialty { get; set; }
        public DbSet<ProviderSubSpecialtyEntity> ProviderSubSpecialty { get; set; }
        public DbSet<ProviderDelegateEntity> ProviderDelegate { get; set; }


        // Insurer
        public DbSet<InsurerEntity> Insurer { get; set; }
        //public DbSet<InsurerTypeEntity> InsurerType { get; set; }
        //public DbSet<InsurerTypeListEntity> InsurerTypeList { get; set; }
        //public DbSet<InsurerCompanyEntity> InsurerCompanyList { get; set; }

        // Delegate
        public DbSet<DelegateEntity> Delegate { get; set; }
        public DbSet<DelegateCompanyEntity> DelegateCompany { get; set; }
        public DbSet<DelegateTypeEntity> DelegateType { get; set; }
        public DbSet<DelegateInviteEmailEntity> DelegateInviteEmails { get; set; }

        // Address
        public DbSet<AddressEntity> Address { get; set; }
        public DbSet<ProviderAddressEntity> ProviderAddress { get; set; }
        public DbSet<MedicalGroupAddressEntity> MedicalGroupAddress { get; set; }
        public DbSet<CorporationAddressEntity> CorporationAddress { get; set; }
        public DbSet<AddressPlanAcceptListEntity> AddressPlanAcceptList { get; set; }
        public DbSet<AddressServiceHourEntity> AddressServiceHour { get; set; }

        // Corporation
        public DbSet<CorporationEntity> Corporation { get; set; }
        public DbSet<CorporationSpecialtyEntity> CorporationSpecialty { get; set; }
        public DbSet<CorporationSubSpecialtyEntity> CorporationSubSpecialty { get; set; }
        public DbSet<ProviderCorporationEntity> ProviderCorporation { get; set; }
        public DbSet<CorporationDocumentEntity> CorporationDocument { get; set; }


        // Hospital
        public DbSet<HospitalEntity> Hospital { get; set; }
        public DbSet<HospPriviledgeListEntity> HospPriviledgeList { get; set; }
        public DbSet<HospitalListEntity> HospitalList { get; set; }

        //public DbSet<EdFellowshipInstitutionEntity> EdFellowshipInstitution { get; set; }
        //public DbSet<EdInternshipInstitutionEntity> EdInternshipInstitution { get; set; }
        //public DbSet<EdMedicalSchoolEntity> EdMedicalSchool { get; set; }
        //public DbSet<EdResidencyInstitutionEntity> EdResidencyInstitution { get; set; }

        //public DbSet<AddressHourPlanEntity> AddressHourPlan { get; set; }
        //public DbSet<AddressTypeListEntity> AddressTypeList { get; set; }


        // NPI
        //public DbSet<PNPIEntity> PNPI { get; set; }
        //public DbSet<PNPITypeListEntity> PNPITypeList { get; set; }

        // Document
        public DbSet<DocumentLocationEntity> DocumentLocation { get; set; }
        public DbSet<DocumentTypeEntity> DocumentType { get; set; }
        public DbSet<DocumentCommentEntity> DocumentComment { get; set; }
        public DbSet<DocumentCommentTypeEntity> DocumentCommentType { get; set; }
        public DbSet<DocumentSectionTypeEntity> DocumentSectionType { get; set; }


        // Medical Group
        public DbSet<MedicalGroupEntity> MedicalGroup { get; set; }
        public DbSet<MedicalGroupTypeEntity> MedicalGroupType { get; set; }
        public DbSet<CareTypeEntity> CareType { get; set; }
        public DbSet<ProviderMedicalGroupEntity> ProviderMedicalGroup { get; set; }



        // Json Provider Draft
        public DbSet<JsonProviderFormEntity> JsonProviderForm { get; set; }
        public DbSet<JsonProviderFormHistoryEntity> JsonProviderFormHistory { get; set; }

        // Specialties
        public DbSet<SpecialtyListEntity> SpecialtyList { get; set; }

        public DbSet<SubSpecialtyListEntity> SubSpecialtyList { get; set; }

        // Education
        public DbSet<EducationInfoEntity> EducationInfo { get; set; }
        public DbSet<MedicalSchoolEntity> MedicalSchool { get; set; }
        //public DbSet<EducationTypeEntity> EducationType{ get; set; }
        public DbSet<BoardEntity> Board { get; set; }
        public DbSet<BoardSpecialtyEntity> BoardSpecialties { get; set; }

        public DbSet<ResidencyEntity> Residency { get; set; }

        // Organization Type
        public DbSet<OrganizationTypeEntity> OrganizationType { get; set; }

        // Medical Licenses
        public DbSet<MedicalLicenseEntity> MedicalLicense { get; set; }

        public DbSet<MedicalLicenseTypeListEntity> MedicalLicenseTypeList { get; set; }

        // App Notification
        public DbSet<AppNotificationEntity> AppNotification { get; set; }
        public DbSet<NotificationProfileCompletionDetailEntity> NotificationProfileCompletionDetail { get; set; }

        public DbSet<AppNotificationTypeListEntity> AppNotificationTypeList { get; set; }

        //SignUpHistory
        public DbSet<SignUpHistoryEntity> SignUpHistory { get; set; }

        // Malpractice
        public DbSet<MalpracticeEntity> Malpractice { get; set; }

        public DbSet<ProfessionalLiabilityEntity> ProfessionalLiability { get; set; }
        
        public DbSet<MalpracticeOIGCaseNumbers> MalpracticeOIGCaseNumbers { get; set; }

        // Lists
        public DbSet<CitizenshipTypeEntity> CitizenshipType { get; set; }
        public DbSet<MalpracticeCarrierListEntity> MalpracticeCarrierList { get; set; }
        public DbSet<ProfessionalLiabilityCarrierListEntity> ProfessionalCarrierList { get; set; }
        public DbSet<EntityTypeEntity> EntityType { get; set; }
        public DbSet<AddressTypeEntity> AddressType { get; set; }
        public DbSet<AddressPrincipalTypeEntity> AddressPrincipalType { get; set; }
        public DbSet<AddressStateEntity> AddressState { get; set; }
        public DbSet<InsuranceTypeEntity> InsuranceType { get; set; }
        public DbSet<CorpTaxIdTypeEntity> CorpTaxIdType { get; set; }
        public DbSet<AddressCountryEntity> AddressCountry { get; set; }
        

        // Attestation
        public DbSet<AttestationEntity> Attestation { get; set; }
        public DbSet<AttestationTypeEntity> AttestationType { get; set; }

        // Period
        public DbSet<PeriodEntity> Period { get; set; }

        // Insurers
        public DbSet<InsurerCompanyEntity> InsurerCompany { get; set; }
        public DbSet<InsurerEmployeeEntity> InsurerEmployee { get; set; }
        public DbSet<InsurerAdminEntity> InsurerAdmin { get; set; }

        // OCS Admin
        public DbSet<OCSAdminEntity> OCSAdmin { get; set; }

        //Notification
        public DbSet<NotificationEntity> Notification { get; set; }
        public DbSet<NotificationTypeEntity> NotificationType { get; set; }
        public DbSet<NotificationStatusEntity> NotificationStatus { get; set; }
        public DbSet<NotificationErrorEntity> NotificationError { get; set; }
        public DbSet<ResourceTypeEntity> ResourceType { get; set; }

        //CredForm and Insurer Flow Status
        public DbSet<CredFormEntity> CredForm { get; set; }
        public DbSet<CredFormStatusTypeEntity> CredFormStatusType { get; set; }
        public DbSet<StateTypeEntity> StateType { get; set; }
        public DbSet<InsurerStatusTypeEntity> InsurerStatusType { get; set; }
        public DbSet<ProviderInsurerCompanyStatusEntity> ProviderInsurerCompanyStatus { get; set; }
        public DbSet<ProviderInsurerCompanyStatusHistoryEntity> ProviderInsurerCompanyStatusHistory { get; set; }

        //ChangeLog
        public DbSet<ChangeLogEntity> ChangeLog { get; set; }
        public DbSet<ChangeLogResourceTypeEntity> ChangeLogResourceType { get; set; }
        public DbSet<ChangeLogUserCaseTypeEntity> ChangeLogUserCaseType { get;set; }

        //Education Document
        public DbSet<MedicalSchoolDocumentEntity> MedicalSchoolDocument { get; set; }
        public DbSet<EducationInfoDocumentEntity> EducationInfoDocument { get; set; }
        public DbSet<BoardDocumentEntity> BoardDocument { get; set; }
    }

}
