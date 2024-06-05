using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class PlanAcceptListEntity : EntityAuditBase
    {
        public int Id { get; set; }
        public string InsurerCompanyId { get; set; }
        public string Name { get; set; }

        #region relationships
        public ICollection<ProviderPlanAcceptEntity> ProviderPlanAccept { get; set; }

        public InsurerCompanyEntity InsurerCompany { get; set; }
        #endregion
    }
}
