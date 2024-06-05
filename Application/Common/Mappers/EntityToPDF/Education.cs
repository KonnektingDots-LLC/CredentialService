using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;

namespace cred_system_back_end_app.Application.Common.Mappers.EntityToPDF
{
    public class Education
    {
        public static EducationSchoolDto GetEducationSchoolDTO(MedicalSchoolEntity medicalSchoolData)
        {
            var medicalSchoolAddressLine = medicalSchoolData.Address;

            return new EducationSchoolDto
            {
                EduSchoolName = medicalSchoolData.Name,
                EduSchAddress = medicalSchoolAddressLine.GetFormattedAddressString(),
                EduSchCityStZipCode = $"{medicalSchoolAddressLine.AddressState.Name}, {medicalSchoolAddressLine.City} {medicalSchoolAddressLine.ZipCode}",

                // TODO: esto no me llega del frontend.
                EduSchGradDateFrom = "",

                EduSchGradDateTo = $"{medicalSchoolData.GraduationMonth}/{medicalSchoolData.GraduationYear}",
                EduSchSpecialty = medicalSchoolData.MSSpecialty,
            };
        }

        public static EducationInternshipDto GetEducationInternshipDto(EducationInfoEntity educationData)
        {
            var educationPeriod = educationData.EducationPeriod;

            return new EducationInternshipDto
            {
                EduInternshipName = educationData.InstitutionName,
                EduInternshipAddress = educationData.Address.GetFormattedAddressString(),
                EduInternshipFrom = educationPeriod.GetFormattedStartDate(),
                EduInternshipTo = educationPeriod.GetFormattedEndDate(),
                EduIntProgramType = educationData.ProgramType,
            };
        }

        public static EducationResidencyDto GetEducationResidencyDto(EducationInfoEntity residencyData)
        {
            var residencyPeriod = residencyData.EducationPeriod;

            return new EducationResidencyDto
            {
                // TODO: que es esto?
                EduResidency = "",

                EduResidencyName = residencyData.InstitutionName,
                EduResidencyAddress = residencyData.Address.GetFormattedAddressString(),
                EduResidencyFrom = residencyPeriod.GetFormattedStartDate(),
                EduResidencyTo = residencyPeriod.GetFormattedEndDate(),

                // TODO: sacar del address
                EduResCityStZipCode = residencyData.Address.GetFormattedCityStateZipCodeCombination(),
                EduResidencyType = residencyData.ProgramType,
                EduResidencyComplDt = residencyData.EducationCompletionDate.ToString(DateFormats.IIPCA_DATE_FROMAT),
                EduResHospComplDt = residencyData.Residency.PostGraduateCompletionDate.ToString(DateFormats.IIPCA_DATE_FROMAT),
            };
        }

        public static EducationFellowshipDto GetEducationFellowshipDto(EducationInfoEntity fellowshipData)
        {
            var residencyPeriod = fellowshipData.EducationPeriod;

            return new EducationFellowshipDto
            {
                EduFellowship = "",
                EduFellowshipName = fellowshipData.InstitutionName,
                EduFellowshipAddres = fellowshipData.Address.GetFormattedAddressString(),
                EduFellowshipCity = fellowshipData.Address.City,
                EduFellowshipFrom = residencyPeriod.GetFormattedStartDate(),
                EduFellowshipTo = residencyPeriod.GetFormattedEndDate(),
                EduFellowshipType = fellowshipData.ProgramType,
                EduFellowshipComplDt = fellowshipData.EducationCompletionDate.ToString(DateFormats.IIPCA_DATE_FROMAT),
            };
        }

        public static EducationBoardDto GetEducationBoardDto(BoardEntity boardData)
        {
            var specialtiesAsString = boardData.Specialty
                .Select(s => s.Name)
                .Aggregate((acc, nextSpecialty) => $"{acc}, {nextSpecialty}");

            return new EducationBoardDto
            {
                // TODO:  get name
                BrdCertification = "",
                BrdSpecialty = specialtiesAsString,
                BrdSpecialtyFrom = boardData.SBCertificateIssuedDate?.ToString(DateFormats.IIPCA_DATE_FROMAT),
                BrdSpecialtyTo = boardData.SBCertificateExpirationDate?.ToString(DateFormats.IIPCA_DATE_FROMAT),
            };
        }


    }
}
