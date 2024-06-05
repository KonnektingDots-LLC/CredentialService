namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ProviderPlanAcceptEntity:RecordHistory
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public ProviderEntity Provider { get; set; }
        public int PlanAcceptListId { get; set; }
        public PlanAcceptListEntity PlanAcceptList { get; set; }
    }
}
