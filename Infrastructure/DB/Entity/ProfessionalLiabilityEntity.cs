namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ProfessionalLiabilityEntity : RecordHistory
    {
        public int Id { get; set; }

        public int ProviderId { get; set; }

        public int ProfessionalLiabilityCarrierId { get; set; }

        public DateTime? InsurancePolicyEffectiveDate { get; set; }

        public DateTime? InsurancePolicyExpirationDate { get; set; }

        public string PolicyNumber { get; set; }

        public string CoverageAmountPerOccurence { get; set; }

        public string CoverageAggregateLimit { get; set; }

        public bool CoverageEvidence { get; set; }

        public bool ActionExplanationFormEvidence { get; set; }

        #region related entities

        public ProviderEntity Provider { get; set; }

        public ProfessionalLiabilityCarrierListEntity ProfessionalLiabilityCarrier { get; set; }

        public string? ProfessionalLiabilityCarrierOther { get; set; }

        #endregion
    }
}
