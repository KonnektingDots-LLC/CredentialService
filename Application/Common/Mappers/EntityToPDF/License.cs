using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;

namespace cred_system_back_end_app.Application.Common.Mappers.EntityToPDF
{
    public class License
    {
        public static LicenseDEADto GetLicenseDEADto(MedicalLicenseEntity deaLicenseData)
        {
            if (deaLicenseData != null)
            {
                return new LicenseDEADto
                {
                    LicDEACert = HasLicense(true),
                    LicDEAExpirationDate = deaLicenseData.MedicalLicenseExpirationDate?.ToString(DateFormats.IIPCA_DATE_FROMAT),
                    LicDEALicenseNumber = deaLicenseData.MedicalLicenseNumber,
                };
            }

            return new LicenseDEADto() { LicDEACert = HasLicense(false) };
        }

        public static LicenseASSMCADto GetLicenseASSMCADto(MedicalLicenseEntity assmcaData)
        {
            if (assmcaData != null)
            {
                return new LicenseASSMCADto
                {
                    LicASSMCACert = HasLicense(true),
                    LicASSMCALicenseNumber = assmcaData.MedicalLicenseNumber,
                    LicASSMCAExpirationDate = assmcaData.MedicalLicenseExpirationDate?.ToString(DateFormats.IIPCA_DATE_FROMAT)
                };
            }

            return new LicenseASSMCADto() { LicASSMCACert = HasLicense(false) };
        }

        public static LicenseMedicalDto GetLicenseMedicalDto(MedicalLicenseEntity deaLicenseData)
        {
            return new LicenseMedicalDto
            {
                LicMedicalLicense = HasLicense(true),
                LicMedicalLicenseNumber = deaLicenseData.MedicalLicenseNumber,
                LicMedicalExpirationDate = deaLicenseData.MedicalLicenseExpirationDate?.ToString(DateFormats.IIPCA_DATE_FROMAT)
            };
        }

        public static LicenseCollegiateMembershipDto GetLicenseCollegiateDto(MedicalLicenseEntity collegiateMembershipLicense)
        {
            if (collegiateMembershipLicense == null)
            {
                return new LicenseCollegiateMembershipDto() { LicCollegiateMember = HasLicense(false) };
            }

            return new LicenseCollegiateMembershipDto
            {
                LicCollegiateMember = HasLicense(true),
                LicCollegiateMemberLicenseNumber = collegiateMembershipLicense.MedicalLicenseNumber,
                LicCollegiateMemberExpirationDate = collegiateMembershipLicense.MedicalLicenseExpirationDate?.ToString(DateFormats.IIPCA_DATE_FROMAT),
            };
        }

        public static LicensePTANDto GetLicensePTANDto(MedicalLicenseEntity ptanLicenseData)
        {
            if (ptanLicenseData != null)
            {
                return new LicensePTANDto
                {
                    LicPTANCProgram = HasLicense(true),
                    LicPTANExpirationDate = ptanLicenseData.MedicalLicenseExpirationDate?.ToString(DateFormats.IIPCA_DATE_FROMAT),
                    LicPTANLicenseNumber = ptanLicenseData.MedicalLicenseNumber,
                };
            }

            return new LicensePTANDto() { LicPTANCProgram = HasLicense(false) };
        }

        public static LicenseTelemedicineDto GetLicenseTelemedicineDto(MedicalLicenseEntity deaLicenseData)
        {
            if (deaLicenseData != null)
            {
                return new LicenseTelemedicineDto
                {
                    LicTelemedicine = HasLicense(deaLicenseData.HasMedicalLicense),
                    LicTelemedicineLicenseNumber = deaLicenseData.MedicalLicenseNumber,
                    LicTelemedicineExpirationDate = deaLicenseData.MedicalLicenseExpirationDate?.ToString(DateFormats.IIPCA_DATE_FROMAT)
                };
            }

            return new LicenseTelemedicineDto() { LicTelemedicine = HasLicense(false) };
        }

        private static string HasLicense(bool hasLicense)
        {
            return hasLicense ? "YES" : "N/A";
        }
    }
}
