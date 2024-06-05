using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class ProviderPlanAcceptEntity : EntityCommon
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public ProviderEntity Provider { get; set; }
        public int PlanAcceptListId { get; set; }
        public PlanAcceptListEntity PlanAcceptList { get; set; }
    }
}
