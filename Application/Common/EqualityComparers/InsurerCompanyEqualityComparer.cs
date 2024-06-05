using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.Common.Comparers
{
    public class InsurerCompanyEqualityComparer : IEqualityComparer<InsurerCompanyEntity>
    {
        public bool Equals(InsurerCompanyEntity? insurer1, InsurerCompanyEntity? insurer2) 
        { 
            if (insurer1.Id == insurer2.Id && insurer1.Name == insurer2.Name) return true;

            return false;
        }

        public int GetHashCode(InsurerCompanyEntity insurer) 
        {
            return $"{insurer.Id + insurer.Name}".GetHashCode();
        }
    }
}
