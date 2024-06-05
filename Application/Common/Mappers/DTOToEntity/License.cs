using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.UseCase.Submit.DTO;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;

namespace cred_system_back_end_app.Application.Common.Mappers.DTOToEntity
{
    public static class License
    {
        public static ICollection<MedicalLicenseEntity> GetMedicalLicenseEntities(LicensesCertificatesDTO licensesData, int providerId)
        {
            var medicalLicenses =  new List<MedicalLicenseEntity>
            {
                GetMedicalLicenseEntityFromPRMedical(licensesData, LicenseCertificatesTypes.PRMedicalLicense, providerId),
            };

            if (licensesData.AssmcaCertificate != null) 
            {
                var assmcaCerificate = GetMedicalLicenseEntity(licensesData.AssmcaCertificate, LicenseCertificatesTypes.ASSMCA, providerId);

                medicalLicenses.Add(assmcaCerificate);
            }

            if (licensesData.DeaCertificate != null)
            {
                var deaCertificate = GetMedicalLicenseEntity(licensesData.DeaCertificate, LicenseCertificatesTypes.DEA, providerId);

                medicalLicenses.Add(deaCertificate);
            }            
            
            if (licensesData.PtanCertificate != null)
            {
                var ptanCertificate = GetMedicalLicenseEntity(licensesData.PtanCertificate, LicenseCertificatesTypes.PTAN, providerId);

                medicalLicenses.Add(ptanCertificate);
            }            
            
            if (licensesData.TelemedicineCertificate != null)
            {
                var telemedicineCertificate = GetMedicalLicenseEntity(licensesData.TelemedicineCertificate, LicenseCertificatesTypes.TELEMEDICINE, providerId);

                medicalLicenses.Add(telemedicineCertificate);
            }

            if (licensesData.MembershipCertificate != null) 
            {
                var collegiateMembership = GetMedicalLicenseEntity(licensesData.MembershipCertificate, LicenseCertificatesTypes.MEMBERSHIP, providerId);

                medicalLicenses.Add(collegiateMembership);
            }

            return medicalLicenses;
        }

        #region helpers

        private static MedicalLicenseEntity GetMedicalLicenseEntity(LicenseDTO licenseDTO, int medicalLicenseType, int providerId)
        {
            return new MedicalLicenseEntity
            {
                ProviderId = providerId,
                HasMedicalLicense = licenseDTO.HaveCertificate != null,
                MedicalLicenseNumber = licenseDTO.CertificateNumber,
                MedicalLicenseExpirationDate = DateTimeHelper.ParseDate(licenseDTO.ExpDate),
                MedicalLicenseTypeId = medicalLicenseType
            };
        }

        private static MedicalLicenseEntity GetMedicalLicenseEntityFromPRMedical(LicensesCertificatesDTO licensesCertificatesDTO, int medicalLicenseType, int providerId)
        {
            return new MedicalLicenseEntity
            {
                ProviderId = providerId,
                MedicalLicenseNumber = licensesCertificatesDTO.PrMedicalLicenseNumber,
                MedicalLicenseExpirationDate = DateTimeHelper.ParseDate(licensesCertificatesDTO.PrMedicalLicenseExpDate),
                MedicalLicenseTypeId = medicalLicenseType
            };
        }

        #endregion
    }
}
