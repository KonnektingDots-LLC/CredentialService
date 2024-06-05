using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class MedicalLicenseEntity : EntityCommon
    {
        public int Id { get; set; }

        public int MedicalLicenseTypeId { get; set; }

        public int ProviderId { get; set; }

        public string MedicalLicenseNumber { get; set; }

        public bool HasMedicalLicense { get; set; }

        public DateTime? MedicalLicenseExpirationDate { get; set; }

        public bool MedicalLicenseEvidence { get; set; }

        #region related entities

        public MedicalLicenseTypeListEntity MedicalLicenseType { get; set; }

        public ProviderEntity Provider { get; set; }

        #endregion
    }
}
