namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class EducationInfoEntity : RecordHistory
    {
        public int Id { get; set; }

        public int EducationTypeId { get; set; }

        public int EducationPeriodId { get; set; }

        public DateTime EducationCompletionDate { get; set; }

        public string InstitutionName { get; set; }

        public int AddressId { get; set; }

        public string? ProgramType { get; set; }

        #region related entities

        public List<ProviderEntity> Provider { get; } = new();
        
        public EducationTypesEntity EducationType { get; set; }

        public PeriodEntity EducationPeriod { get; set; } = null!;

        public AddressEntity Address { get; set; } = null!;

        public ResidencyEntity Residency { get; set; }

        public EducationInfoDocumentEntity EducationInfoDocument { get; set; }
        #endregion
    }
}
