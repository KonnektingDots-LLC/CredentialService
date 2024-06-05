using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.Common.EqualityComparers
{
    public class ProviderSpecialtyComparer : IEqualityComparer<ProviderSpecialtyEntity>
    {
        public bool Equals(ProviderSpecialtyEntity ps1, ProviderSpecialtyEntity ps2) 
        { 
            if (ps1.ProviderId == ps2.ProviderId 
                && ps1.SpecialtyListId == ps2.SpecialtyListId
                && ps1.AzureBlobFileName == ps2.AzureBlobFileName) 
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(ProviderSpecialtyEntity obj) 
        { 
            return obj.SpecialtyListId.GetHashCode(); 
        }
    }
}
