using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PDFMapper = cred_system_back_end_app.Application.Common.Mappers.EntityToPDF;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    // TODO: Seperate CRUD logic into appropriate repositories for each entity
    // then remove DBContextEntity from this class.
    public class PDFDataRepository
    {
        private DbContextEntity _dbContextEntity;

        public PDFDataRepository(DbContextEntity dbContextEntity)
        {
            _dbContextEntity = dbContextEntity;
        }

        public void SetDbContextTransaction(DbContextEntity dbContextEntity)
        {
            _dbContextEntity = dbContextEntity;
        }

        public IndPrimaryPracticeProfile1Dto GetIndividualPracticeProfile(int providerId)
        {

            var providerProfileData = _dbContextEntity.Provider
                .Include(p => p.ProviderDetail)
                .Include(p => p.Address)
                .ThenInclude(addr => addr.AddressState)
                .Where(p => p.Id == providerId)
                .ToList()
                .FirstOrDefault();

            var providerSubSpecialty = _dbContextEntity.ProviderSubSpecialty
                .Where(ps => ps.ProviderId == providerId)
                .Include(ps => ps.SubSpecialtyList)
                .ToList()
                .FirstOrDefault();

            var providerDTO = PDFMapper.Provider
                .GetIndPrimaryPracticeProfileDTO(
                    providerProfileData,
                    providerSubSpecialty?.SubSpecialtyList?.Name
                );

            return providerDTO;
        }

        public (CorporatePracticeProfile2Dto, List<AdditionalCorporatePracticeProfileDto>) GetCorporatePracticeProfile(int providerId)
        {
            var corporations = _dbContextEntity.ProviderCorporation
                .Include(pc => pc.Corporation)
                .ThenInclude(c => c.EntityType)
                .Include(pc => pc.Corporation)
                .ThenInclude(c => c.SubSpecialty)
                .Include(pc => pc.Corporation)
                .ThenInclude(c => c.Address)
                .Where(pc => pc.ProviderId == providerId)
                .AsSplitQuery()
                .ToList()
                ?.Select(pc => pc.Corporation)
                ?.ToArray();

            var primaryCorporatePracticeProfile = PDFMapper.Corporation.GetCorporatePracticeProfile2DTO(corporations.First());

            var additionalCorporatePractiProfiles = corporations
                .Skip(1)
                .Select(corp => PDFMapper.Corporation.GetAdditionalCorporatePracticeProfileDto(corp))
                .ToList();

            return (primaryCorporatePracticeProfile, additionalCorporatePractiProfiles);
        }

        public async Task<PrimaryCarePhysicianPCPDto> GetPCPProfile(int providerId)
        {
            var pcpData = await _dbContextEntity.ProviderMedicalGroup
                .Where(pm => pm.ProviderId == providerId)
                .Include(pm => pm.MedicalGroup)
                .ThenInclude(mg => mg.Address)
                .ThenInclude(a => a.AddressState)
                .ThenInclude(mg => mg.Address)
                .ThenInclude(a => a.AddressServiceHours)
                .Include(pm => pm.MedicalGroup)
                .ThenInclude(mg => mg.CareType)
                .Select(pcp => pcp.MedicalGroup)
                .Where(mg => mg.MedicalGroupTypeId == MedicalGroupTypes.PCP)
                .FirstOrDefaultAsync();

            var specialty = _dbContextEntity.SpecialtyList
                .Where(s => s.Id == pcpData.SpecifyPrimaryCareId)
                .ToList()
                .FirstOrDefault()
                ?.Name;

            return PDFMapper.MedicalGroup.GetPrimaryCarePhysicianPCPDTO(pcpData, specialty);
        }

        public FederalQualifiedHealthCenter330Dto GetF330Profile(int providerId)
        {
            var f330Data = _dbContextEntity.ProviderMedicalGroup
                .Where(pm => pm.ProviderId == providerId)
                .Include(pm => pm.MedicalGroup)
                .ThenInclude(mg => mg.Address)
                .ThenInclude(a => a.AddressServiceHours)
                .Include(pm => pm.MedicalGroup)
                .ThenInclude(mg => mg.CareType)
                .Include(pm => pm.MedicalGroup)
                .ToList()
                .Select(pcp => pcp.MedicalGroup)
                .Where(mg => mg.MedicalGroupTypeId == MedicalGroupTypes.F330)
                .First();

            var careId = f330Data.CareTypeId == CareTypes.Specialist ?
                f330Data.TypeOfSpecialistId :
                f330Data.SpecifyPrimaryCareId;

            var specialty = _dbContextEntity.SpecialtyList
                .Where(s => s.Id == careId)
                .ToList()
                .FirstOrDefault()
                ?.Name;

            return PDFMapper.MedicalGroup.GetF330DTO(f330Data, specialty);
        }

        public HospitalAffiliationsDto GetHospitalAffiliations(int providerId)
        {
            var hospitalsData = _dbContextEntity.ProviderHospital
                .Where(ph => ph.ProviderId == providerId)
                .Include(ph => ph.Hospital)
                .ThenInclude(h => h.HospitalList)
                .Include(ph => ph.Hospital)
                .ThenInclude(h => h.HospPriviledgeList)
                .Include(ph => ph.Hospital)
                .ThenInclude(h => h.HospitalPriviledgePeriod)
                .AsSplitQuery()
                .Select(ph => ph.Hospital);

            return PDFMapper.Hospital.GetHospitalAffiliationsDTO(hospitalsData);
        }

        public EducationAndTrainingDto GetEducationAndTrainingDto(int providerId)
        {
            var educationInfos = _dbContextEntity
                  .ProviderEducationInfo
                  .Where(p => p.ProviderId == providerId)
                  .Include(pe => pe.EducationInfo)
                  .ThenInclude(ei => ei.EducationPeriod)
                  .Include(pe => pe.EducationInfo)
                  .ThenInclude(ei => ei.Address)
                  .Include(ei => ei.EducationInfo.Residency)
                  .ToList()
                  .Select(pe => pe.EducationInfo);

            var medicalSchools = GetMedicalSchools(providerId);

            var internshipDtos = GetInternships(educationInfos);

            var residencyDtos = GetResidencies(educationInfos);

            var fellowshipDtos = GetFellowships(educationInfos);

            var boards = GetBoards(providerId);

            return new EducationAndTrainingDto
            {
                EducationSchool = medicalSchools,
                EducationInternship = internshipDtos,
                EducationResidency = residencyDtos,
                EducationFellowship = fellowshipDtos,
                EducationBoard = boards
            };
        }

        public EducationSchoolDto[] GetMedicalSchools(int providerId)
        {
            var medicalSchools = _dbContextEntity.MedicalSchool
                .Where(ms => ms.ProviderId == providerId)
                .Include(ms => ms.Address)
                .ThenInclude(a => a.AddressState)
                .ToList();

            return medicalSchools
                    .Select(m => PDFMapper.Education.GetEducationSchoolDTO(m))
                    .ToArray();
        }

        public EducationInternshipDto[] GetInternships(IEnumerable<EducationInfoEntity> educationInfos)
        {
            var internships = educationInfos
                .Where(e => e.EducationTypeId == EducationTypes.Internship);

            if (!internships.IsNullOrEmpty())
            {
                return internships
                        .Select(i => PDFMapper.Education.GetEducationInternshipDto(i))
                        .ToArray();
            }

            return new EducationInternshipDto[] { };
        }

        public EducationResidencyDto[] GetResidencies(IEnumerable<EducationInfoEntity> educationInfos)
        {
            var residencies = educationInfos
                .Where(e => e.EducationTypeId == EducationTypes.Residency)
                .DistinctBy(x => x.Id);

            if (!residencies.IsNullOrEmpty())
            {
                return residencies
                        .Select(i => PDFMapper.Education.GetEducationResidencyDto(i))
                        .ToArray();
            }

            return new EducationResidencyDto[] { };
        }

        public EducationFellowshipDto[] GetFellowships(IEnumerable<EducationInfoEntity> educationInfos)
        {
            var fellowships = educationInfos
                .Where(e => e.EducationTypeId == EducationTypes.Fellowship);

            if (!fellowships.IsNullOrEmpty())
            {
                return fellowships
                        .Select(i => PDFMapper.Education.GetEducationFellowshipDto(i))
                        .ToArray();
            }

            return new EducationFellowshipDto[] { };
        }

        public EducationBoardDto[] GetBoards(int providerId)
        {
            var boards = _dbContextEntity.Board
                .Where(b => b.ProviderId == providerId)
                .Include(b => b.Specialty)
                .ToList();

            if (!boards.IsNullOrEmpty())
            {
                return boards
                    .Select(b => PDFMapper.Education.GetEducationBoardDto(b))
                    .ToArray();
            }

            return new EducationBoardDto[] { };
        }

        public LicenseAndCertificationDto GetLicenseAndCertificationDto(int providerId)
        {
            var licenses = _dbContextEntity
                .MedicalLicense
                .Where(ml => ml.ProviderId == providerId)
                .ToList();

            var deaLicenseDto = GetDEALicense(licenses);
            var asssmcaLicenseDto = GetASSMCALicense(licenses);
            var prMedicalLicenseDto = GetMedicalLicense(licenses);
            var membershipLicenseDto = GetMembershipLicense(licenses);
            var ptanLicenseDto = GetPTANLicense(licenses);
            var telemedicineLicenseDto = GetTelemedicineLicense(licenses);

            return new LicenseAndCertificationDto
            {
                LicenseDEA = deaLicenseDto,
                LicenseASSMCA = asssmcaLicenseDto,
                LicenseMedical = prMedicalLicenseDto,
                LicenseCollegiateMembership = membershipLicenseDto,
                LicensePTAN = ptanLicenseDto,
                LicenseTelemedicine = telemedicineLicenseDto,
            };
        }

        public LicenseDEADto GetDEALicense(ICollection<MedicalLicenseEntity> medicalLicenseEntities)
        {
            var deaLicense = medicalLicenseEntities
                .Where(m => m.MedicalLicenseTypeId == LicenseCertificatesTypes.DEA)
                .FirstOrDefault();

            return PDFMapper.License.GetLicenseDEADto(deaLicense);
        }

        public LicenseASSMCADto GetASSMCALicense(ICollection<MedicalLicenseEntity> medicalLicenseEntities)
        {
            var assmcLicense = medicalLicenseEntities
                .Where(m => m.MedicalLicenseTypeId == LicenseCertificatesTypes.ASSMCA)
                .FirstOrDefault();

            return PDFMapper.License.GetLicenseASSMCADto(assmcLicense);
        }

        public LicenseMedicalDto GetMedicalLicense(ICollection<MedicalLicenseEntity> medicalLicenseEntities)
        {
            var medicalLicense = medicalLicenseEntities
                .Where(m => m.MedicalLicenseTypeId == LicenseCertificatesTypes.PRMedicalLicense)
                .FirstOrDefault();

            return PDFMapper.License.GetLicenseMedicalDto(medicalLicense);
        }

        public LicenseCollegiateMembershipDto GetMembershipLicense(ICollection<MedicalLicenseEntity> medicalLicenseEntities)
        {
            var membershipLicense = medicalLicenseEntities
                .Where(m => m.MedicalLicenseTypeId == LicenseCertificatesTypes.MEMBERSHIP)
                .FirstOrDefault();

            return PDFMapper.License.GetLicenseCollegiateDto(membershipLicense);
        }

        public LicensePTANDto GetPTANLicense(ICollection<MedicalLicenseEntity> medicalLicenseEntities)
        {
            var PTANLicense = medicalLicenseEntities
                .Where(m => m.MedicalLicenseTypeId == LicenseCertificatesTypes.PTAN)
                .FirstOrDefault();

            return PDFMapper.License.GetLicensePTANDto(PTANLicense);
        }

        public LicenseTelemedicineDto GetTelemedicineLicense(ICollection<MedicalLicenseEntity> medicalLicenseEntities)
        {
            var telemedicineLicense = medicalLicenseEntities
                .Where(m => m.MedicalLicenseTypeId == LicenseCertificatesTypes.TELEMEDICINE)
                .FirstOrDefault();

            return PDFMapper.License.GetLicenseTelemedicineDto(telemedicineLicense);
        }


        public NegativeCertificatePenalRecordDateDto GetNegeativePenalRecordCertificate(int providerId)
        {
            //var negativePenalCertRecordDate = _dbContextEntity.ProviderDetail
            //    .Where(pd => pd.ProviderId == providerId)
            //    .FirstOrDefault()
            //    .NegativePenalCertificateExpDate;

            var negativePenalCertRecordDate = _dbContextEntity.DocumentLocation
                .Where(r => r.ProviderId == providerId && r.DocumentTypeId == 21)
                .FirstOrDefault()
                .ExpirationDate;

            return new NegativeCertificatePenalRecordDateDto()
            {
                NegCertPenalRecDate = negativePenalCertRecordDate?.ToString(DateFormats.IIPCA_DATE_FROMAT)
            };
        }

        public MalpracticeDto GetMalpracticeData(int providerId)
        {
            var malpracticeCase = _dbContextEntity.Malpractice
                .Where(m => m.ProviderId == providerId)
                .Include(m => m.MalpracticeCarrier)
                .Include(m => m.MalpracticeOIGCaseNumbers)
                .ToList()
                .Last();

            string oigCaseNumbers = "";

            if (!malpracticeCase.MalpracticeOIGCaseNumbers.IsNullOrEmpty())
            {
                oigCaseNumbers = malpracticeCase.MalpracticeOIGCaseNumbers
                     .Aggregate("", (acc, caseNumber) => $"{acc}{caseNumber.OIGCaseNumber} ");
            }

            return PDFMapper.Insurance.GetMalpracticeDto(malpracticeCase, oigCaseNumbers);
        }

        public ProfessionalLiabilityDto GetProfessionalLiabilityData(int providerId)
        {
            var professionalLiability = _dbContextEntity.ProfessionalLiability
                .Where(m => m.ProviderId == providerId)
                .Include(m => m.ProfessionalLiabilityCarrier)
                .ToList()
                .FirstOrDefault();

            if (professionalLiability != null)
            {
                return PDFMapper.Insurance.GetProfessionalLiabilityDto(professionalLiability);
            }

            return new ProfessionalLiabilityDto();
        }

        public AdditionalDirectoryDto GetAdditionalProviderData()
        {
            return new AdditionalDirectoryDto { };
        }
    }
}
