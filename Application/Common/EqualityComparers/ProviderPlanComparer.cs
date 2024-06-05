using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.Common.EqualityComparers
{
    public class ProviderPlanComparer : IEqualityComparer<ProviderPlanAcceptEntity>
    {
        public bool Equals(ProviderPlanAcceptEntity p1, ProviderPlanAcceptEntity p2)
        {
            if (p1.PlanAcceptListId == p2.PlanAcceptListId && p1.ProviderId == p2.ProviderId)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(ProviderPlanAcceptEntity obj)
        {
            return obj.PlanAcceptListId.GetHashCode();
        }
    }
}
