using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Application.Common.EqualityComparers
{
    public class OIGNumberComparer : IEqualityComparer<MalpracticeOIGCaseNumbers>
    {
        public bool Equals(MalpracticeOIGCaseNumbers m1, MalpracticeOIGCaseNumbers m2)
        {
            if (m1.MalpracticeId == m2.MalpracticeId && m1.OIGCaseNumber == m2.OIGCaseNumber) return true;
            return false;
        }

        public int GetHashCode(MalpracticeOIGCaseNumbers m)
        {
            return m.MalpracticeId.GetHashCode();
        }
    }
}
