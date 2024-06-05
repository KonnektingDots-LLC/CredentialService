using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.Common.EqualityComparers
{
    public class CorporationSubSpecialtyComparer : IEqualityComparer<CorporationSubSpecialtyEntity>
    {
        public bool Equals(CorporationSubSpecialtyEntity cs1, CorporationSubSpecialtyEntity cs2) 
        {
            if (cs1.SubSpecialtyListEntityId == cs2.SubSpecialtyListEntityId
                && cs1.CorporationEntityId == cs2.CorporationEntityId)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(CorporationSubSpecialtyEntity cs1) 
        { 
            return cs1.SubSpecialtyListEntityId.GetHashCode(); 
        }
    }
}
