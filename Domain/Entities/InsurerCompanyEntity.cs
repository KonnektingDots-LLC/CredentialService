using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class InsurerCompanyEntity : EntityCommon
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NotificationEmail { get; set; }

        #region relationships

        public ICollection<InsurerAdminEntity> InsurerAdmins { get; set; }
        public ICollection<InsurerEmployeeEntity> InsurerEmployees { get; set; }
        public ICollection<PlanAcceptListEntity> AcceptedPlans { get; set; }
        public ICollection<ProviderInsurerCompanyStatusEntity> ProviderInsurerCompanyStatus { get; set; }

        #endregion
    }
}
