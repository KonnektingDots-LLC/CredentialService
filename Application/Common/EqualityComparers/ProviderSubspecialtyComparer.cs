using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.Common.EqualityComparers
{
    public class ProviderSubspecialtyComparer : IEqualityComparer<ProviderSubSpecialtyEntity>
    {
        public bool Equals(ProviderSubSpecialtyEntity ps1, ProviderSubSpecialtyEntity ps2) 
        { 
            if (ps1.ProviderId == ps2.ProviderId 
                && ps1.SubSpecialtyListId == ps2.SubSpecialtyListId
                && ps1.DocumentLocationId == ps2.DocumentLocationId) 
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(ProviderSubSpecialtyEntity obj) 
        { 
            return obj.SubSpecialtyListId.GetHashCode(); 
        }
    }
}
