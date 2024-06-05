using cred_system_back_end_app.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Domain.Entities
{
    public class ProviderDetailEntity : EntityCommon
    {
        public int Id { get; set; }

        public int ProviderId { get; set; }

        public int CitizenshipTypeId { get; set; }

        public string TaxId { get; set; }

        [Required, MaxLength(12)]
        public string PRMedicalLicenseNumber { get; set; }

        //public string NpiCertificateNumber { get; set; }

        //public DateTime NegativePenalCertificateIssuedDate { get; set; }

        //public DateTime NegativePenalCertificateExpDate { get; set; }

        public string SSN { get; set; }

        public int IdType { get; set; }

        public DateTime IdExpDate { get; set; }

        public bool Blocked { get; set; } = false;

        public bool UnderInvestigation { get; set; } = false;

        #region Relationship

        public ProviderEntity Provider { get; set; }

        public CitizenshipTypeEntity CitizenshipType { get; set; }

        #endregion

    }
}
