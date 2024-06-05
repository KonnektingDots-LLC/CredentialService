using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Application.Common.EqualityComparers
{
    public class BoardSpecialtyComparer : IEqualityComparer<BoardSpecialtyEntity>
    {
        public bool Equals(BoardSpecialtyEntity bs1, BoardSpecialtyEntity bs2)
        {
            if (bs1.BoardId == bs2.BoardId && bs1.SpecialtyId == bs2.SpecialtyId)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(BoardSpecialtyEntity obj)
        {
            return obj.SpecialtyId.GetHashCode();
        }
    }
}
