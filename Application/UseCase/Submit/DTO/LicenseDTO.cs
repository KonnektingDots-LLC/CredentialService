using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Application.UseCase.Submit.DTO
{
    public class LicenseDTO
    {
        public string HaveCertificate { get; set; }

        [MaxLength(15), MinLength(5)]
        public string CertificateNumber { get; set; }
        public FileBaseDTO CertificateFile { get; set; }
        public string ExpDate { get; set; }
    }

    public class LicensesCertificatesDTO
    {
        [MaxLength(15), MinLength(5)]
        public string PrMedicalLicenseNumber { get; set; }
        public string PrMedicalLicenseExpDate { get; set; }
        public FileBaseDTO PrMedicalLicenseFile { get; set; }
        public LicenseDTO? MembershipCertificate { get; set; }
        public LicenseDTO? AssmcaCertificate { get; set; }
        public LicenseDTO? DeaCertificate { get; set; }
        public LicenseDTO? PtanCertificate { get; set; }
        public LicenseDTO? TelemedicineCertificate { get; set; }
    }
}
