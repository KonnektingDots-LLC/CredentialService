using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;
using PDFMapper = cred_system_back_end_app.Application.Common.Mappers.EntityToPDF;

namespace cred_system_back_end_app.Application.Common.Builders
{
    public class PDFDTOBuilder
    {
        private IIPCAPdfRootDto _currentPDFDTO;

        public PDFDTOBuilder() 
        {
            this.Reset();
        }

        public IIPCAPdfRootDto Build()
        {
            return _currentPDFDTO;
        }

        public PDFDTOBuilder Reset()
        {
            _currentPDFDTO = new IIPCAPdfRootDto
            {
                FormSections = new FormSectionsDto
                {
                    IndPrimaryPracticeProfile1 = new IndPrimaryPracticeProfile1Dto(),
                    CorporatePracticeProfile2 = new CorporatePracticeProfile2Dto(),
                    PrimaryCarePhysicianPCP = new PrimaryCarePhysicianPCPDto(),
                    FederalQualifiedHealthCenter330 = new FederalQualifiedHealthCenter330Dto(),
                    HospitalAffiliations = new HospitalAffiliationsDto(),
                    NegativeCertificatePenalRecordDate = new NegativeCertificatePenalRecordDateDto(),
                    Malpractice = new MalpracticeDto(),
                    ProfessionalLiability = new ProfessionalLiabilityDto(),
                    AdditionalDirectory = new AdditionalDirectoryDto(),
                }
            };

            return this;
        }

        public PDFDTOBuilder WithProvider(ProviderEntity providerData, string subSpecialtyName) 
        {
            _currentPDFDTO.FormSections.IndPrimaryPracticeProfile1 = PDFMapper.Provider
                .GetIndPrimaryPracticeProfileDTO(providerData, subSpecialtyName);

            return this;
        }        
        
        public PDFDTOBuilder WithCorporation(CorporationEntity corporationEntity, string entityType) 
        {
            _currentPDFDTO.FormSections.CorporatePracticeProfile2 = PDFMapper.Corporation
                .GetCorporatePracticeProfile2DTO(corporationEntity);

            return this;
        }        
        
        //public PDFDTOBuilder WithPCP(MedicalGroupEntity medicalGroupEntity) 
        //{
        //    _currentPDFDTO.FormSections.PrimaryCarePhysicianPCP = PDFMapper.MedicalGroup
        //        .GetPrimaryCarePhysicianPCPDTO(medicalGroupEntity);

        //    return this;
        //}        
        
        //public PDFDTOBuilder WithF330(MedicalGroupEntity medicalGroupEntity) 
        //{
        //    _currentPDFDTO.FormSections.FederalQualifiedHealthCenter330 = PDFMapper.MedicalGroup
        //        .GetF330DTO(medicalGroupEntity);

        //    return this;
        //}        
        
        public PDFDTOBuilder WithHospitals(IEnumerable<HospitalEntity> hospitals) 
        {
            _currentPDFDTO.FormSections.HospitalAffiliations = PDFMapper.Hospital
                .GetHospitalAffiliationsDTO(hospitals);

            return this;
        }

        public PDFDTOBuilder WithEducation(
            MedicalSchoolEntity[] medicalSchools,
            EducationInfoEntity[] internships,
            EducationInfoEntity[] residencies,
            EducationInfoEntity[] fellowships,
            BoardEntity[] boards)
        {
            _currentPDFDTO.FormSections.EducationAndTraining = new EducationAndTrainingDto
            {
                EducationSchool = medicalSchools.Select(m => PDFMapper.Education.GetEducationSchoolDTO(m)).ToArray(),
                EducationInternship = internships.Select(i => PDFMapper.Education.GetEducationInternshipDto(i)).ToArray(),
                EducationResidency = residencies.Select(r => PDFMapper.Education.GetEducationResidencyDto(r)).ToArray(),
                EducationFellowship = fellowships.Select(f => PDFMapper.Education.GetEducationFellowshipDto(f)).ToArray(),
                EducationBoard = boards.Select(b => PDFMapper.Education.GetEducationBoardDto(b)).ToArray()
            };

            return this;
        }        
        
        public PDFDTOBuilder WithLicenses(
            MedicalLicenseEntity medicalLicense,
            MedicalLicenseEntity deaLicense,
            MedicalLicenseEntity assmcaLicense,
            MedicalLicenseEntity membershipLicense,
            MedicalLicenseEntity ptanLicense,
            MedicalLicenseEntity telemedicine)
        {
            _currentPDFDTO.FormSections.LicenseAndCertification = new LicenseAndCertificationDto
            {
                LicenseMedical = PDFMapper.License.GetLicenseMedicalDto(medicalLicense),
                LicenseASSMCA = PDFMapper.License.GetLicenseASSMCADto(assmcaLicense),
                LicenseCollegiateMembership = PDFMapper.License.GetLicenseCollegiateDto(membershipLicense),
                LicenseDEA = PDFMapper.License.GetLicenseDEADto(deaLicense),
                LicensePTAN = PDFMapper.License.GetLicensePTANDto(ptanLicense),
                LicenseTelemedicine = PDFMapper.License.GetLicenseTelemedicineDto(telemedicine),
            };

            return this;
        }

        // TODO
        public PDFDTOBuilder WithNegativePenalRecord(string something) 
        { 
            return this;
        }

        //public PDFDTOBuilder WithInsurance(MalpracticeEntity malpractice, ProfessionalLiabilityEntity professionalLiabilityEntity)
        //{
        //    _currentPDFDTO.FormSections.Malpractice = PDFMapper.Insurance.GetMalpracticeDto(malpractice);
        //    _currentPDFDTO.FormSections.ProfessionalLiability = PDFMapper.Insurance.GetProfessionalLiabilityDto(professionalLiabilityEntity);

        //    return this;
        //}

        // TODO
        public PDFDTOBuilder WithAdditional(string something) 
        {
            return this;
        }
    }
}
