using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class MedicalLicenseTypeListEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }

        public int MedicalLicenseTypeName { get; set; }

        #region related entities

        public ICollection<MedicalLicenseEntity> MedicalLicenses { get; set; }

        #endregion
    }
}
