namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class MedicalGroupAddressEntity : OrganizationAddressBase
    {
        public int MedicalGroupId { get; set; }

        #region

        public MedicalGroupEntity MedicalGroup { get; set; }

        #endregion
    }
}
