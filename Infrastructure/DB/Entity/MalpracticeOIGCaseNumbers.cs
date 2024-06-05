namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class MalpracticeOIGCaseNumbers : RecordHistory
    {
        public int? MalpracticeId { get; set; }

        public string OIGCaseNumber { get; set; }

        #region relationships

        public MalpracticeEntity? Malpractice { get; set; }
        #endregion 
    }
}
