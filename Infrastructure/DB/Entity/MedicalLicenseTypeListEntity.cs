namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class MedicalLicenseTypeListEntity : RecordHistoryTypeList
    {
        public int Id { get; set; }

        public int MedicalLicenseTypeName { get; set; }

        #region related entities

        public ICollection<MedicalLicenseEntity> MedicalLicenses { get; set; }

        #endregion
    }
}
