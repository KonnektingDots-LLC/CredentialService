using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class MalpracticeOIGCaseNumbers : EntityCommon
    {
        public int? MalpracticeId { get; set; }

        public string OIGCaseNumber { get; set; }

        #region relationships

        public MalpracticeEntity? Malpractice { get; set; }
        #endregion 
    }
}
