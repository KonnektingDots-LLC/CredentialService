using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Application.Common.EqualityComparers
{
    public class ListMemberEntityComparer<T> : IEqualityComparer<T> where T : ListMemberEntityBase
    {
        public bool Equals(T object1, T object2)
        {
            if (object1.PublicId == object2.PublicId)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(T obj) => obj.PublicId.GetHashCode();
    }
}
