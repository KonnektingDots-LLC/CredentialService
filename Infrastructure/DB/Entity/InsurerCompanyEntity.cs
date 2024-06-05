namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class InsurerCompanyEntity : RecordHistory
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
