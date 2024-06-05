namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class MalpracticeEntity : RecordHistory
    {
        public int Id { get; set; }

        public int ProviderId { get; set; }

        public int MalpracticeCarrierId { get; set; }

        public DateTime? InsurancePolicyEffectiveDate { get; set; }

        public DateTime? InsurancePolicyExpirationDate { get; set; }

        public string PolicyNumber { get; set; }

        public string CoverageAmountPerOcurrence { get; set; }

        public string CoverageAggregateLimit { get; set; }

        #region related entities

        public ProviderEntity Provider { get; set; } 

        public MalpracticeCarrierListEntity MalpracticeCarrier { get; set; }

        public ICollection<MalpracticeOIGCaseNumbers> MalpracticeOIGCaseNumbers { get; set; }

        public string? MalpracticeCarrierOther { get; set; }

        #endregion
    }
}
