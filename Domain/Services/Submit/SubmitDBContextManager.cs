using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.Common.Mappers.DTOToEntity;
using cred_system_back_end_app.Domain.Common;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Domain.Services.Submit.DTO;
using cred_system_back_end_app.Domain.Services.Submit.DTO.EducationDTOs;
using cred_system_back_end_app.Domain.Services.Submit.ModificationServices;
using cred_system_back_end_app.Domain.Services.Submit.ModificationServices.EducationModificationServices;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Domain.Services.Submit
{
    public class SubmitDBContextManager
    {
        private readonly DbContextEntity _dbContextEntity;
        private readonly IndividualPracticeProfileModificationService _individualPracticeProfileResubmitService;
        private readonly AddressAndLocationModificationService _addressAndLocationResubmitService;
        private readonly IncorporatedProfileModificationService _incorporatedProfileResubmitService;
        private readonly MedicalGroupModificationService _medicalGroupModificationService;
        private readonly HospitalAffiliationModificationService _hospitalAffiliationModificationService;
        private readonly MedicalSchoolModificationService _medicalSchoolResubmitService;
        private readonly InternshipModificationService _internshipResubmitService;
        private readonly FellowshipModificationService _fellowshipResubmitService;
        private readonly ResidencyModificationService _residencyResubmitService;
        private readonly BoardModificationService _boardResubmitService;
        private readonly ProfessionalLiabilityModificationService _professionalLiabilityModificationService;
        private readonly MalpracticeModificationService _malpracticeModificationService;
        private readonly SpecialtiesModificationService _specialtiesModificationService;
        private readonly LicensesModificationService _licensesModificationService;
        private readonly IChangeLogRepository _changeLogServiceBase;
        private readonly IDocumentLocationRepository _documentLocationRepository;
        private DateTime _submitDate;
        private List<int> _providerInsurerCompanyStatusIds;

        public SubmitDBContextManager(
            DbContextEntity dbContextEntity,
            IndividualPracticeProfileModificationService individualPracticeProfileResubmitService,
            SpecialtiesModificationService specialtiesModificationService,
            AddressAndLocationModificationService addressAndLocationResubmitService,
            IncorporatedProfileModificationService incorporatedProfileResubmitService,
            MedicalGroupModificationService medicalGroupModificationService,
            HospitalAffiliationModificationService hospitalAffiliationModificationService,
            MedicalSchoolModificationService medicalSchoolResubmitService,
            InternshipModificationService internshipResubmitService,
            FellowshipModificationService fellowshipResubmitService,
            ResidencyModificationService residencyResubmitService,
            BoardModificationService boardResubmitService,
            LicensesModificationService licensesModificationService,
            ProfessionalLiabilityModificationService professionalLiabilityModificationService,
            MalpracticeModificationService malpracticeModificationService,
            IChangeLogRepository changeLogServiceBase,
            IDocumentLocationRepository documentLocationRepository
        )
        {
            _dbContextEntity = dbContextEntity;
            _individualPracticeProfileResubmitService = individualPracticeProfileResubmitService;
            _addressAndLocationResubmitService = addressAndLocationResubmitService;
            _incorporatedProfileResubmitService = incorporatedProfileResubmitService;
            _medicalGroupModificationService = medicalGroupModificationService;
            _hospitalAffiliationModificationService = hospitalAffiliationModificationService;
            _medicalSchoolResubmitService = medicalSchoolResubmitService;
            _internshipResubmitService = internshipResubmitService;
            _fellowshipResubmitService = fellowshipResubmitService;
            _residencyResubmitService = residencyResubmitService;
            _boardResubmitService = boardResubmitService;
            _professionalLiabilityModificationService = professionalLiabilityModificationService;
            _malpracticeModificationService = malpracticeModificationService;
            _specialtiesModificationService = specialtiesModificationService;
            _licensesModificationService = licensesModificationService;
            _changeLogServiceBase = changeLogServiceBase;
            _documentLocationRepository = documentLocationRepository;
        }

        public void SetSubmitDate(DateTime submitDate)
        {
            _submitDate = submitDate;
        }

        public List<int> GetProviderInsurerCompanyStatusIds()
        {
            return _providerInsurerCompanyStatusIds;
        }

        public async Task AddAllEntities(SubmitRequestDTO submitData, string createdBy)
        {
            var providerId = submitData.Content.Setup.ProviderId;

            await AddProviderProfileEntities(submitData, providerId);

            if (!submitData.Content.IncorporatedPracticeProfile.IsNullOrEmpty())
            {
                await AddCorporationEntities(submitData, providerId);
            }

            await AddMedicalGroupsEntities(submitData, providerId);

            await AddEducationEntities(submitData, providerId);

            if (submitData.Content.Setup.HospitalAffiliationsApplies)
            {
                await AddHospitalEntities(submitData, providerId);
            }

            await ModifyDocumentLocationCriminalRecord(submitData.Content.CriminalRecord, providerId, createdBy);

            if (submitData.Content.Setup.InsuranceApplies)
            {
                await AddInsuranceEntities(submitData, providerId);
            }

            await AddAttestationEntities(submitData);

            SetCreatedByColumns(createdBy);

            await AddChangeLog(ChangeLogUseCases.Submit, createdBy, providerId);
        }

        public async Task ModifyEntities(SubmitRequestDTO submitData, string modifiedBy)
        {
            var providerId = submitData.Content.Setup.ProviderId;

            await ModifyProviderDetailEntities(submitData, providerId);

            await ModifyCorporationEntities(submitData, providerId);

            await ModifyMedicalGroupEntities(submitData, providerId);

            await ModifyHospitalEntities(submitData, providerId);

            await ModifyEducationEntities(providerId, submitData.Content.EducationAndTraining);

            await ModifyDocumentLocationCriminalRecord(submitData.Content.CriminalRecord, providerId, modifiedBy);

            await ModifyInsuranceEntities(submitData, providerId);

            await ModifyAttestation(submitData.Content.Attestation.AttestationDate, providerId, modifiedBy);

            SetModifiedByColumns(modifiedBy);

            await AddChangeLog(ChangeLogUseCases.ReSubmit, modifiedBy, providerId);
        }

        private async Task ModifyAttestation(string AttestationDate, int providerId, string modifiedBy)
        {
            var attestation = await _dbContextEntity.Attestation.Where(r => r.ProviderId == providerId).FirstOrDefaultAsync();
            attestation.AttestationDate = DateTimeHelper.ParseDate(AttestationDate);
            attestation.ModifiedBy = modifiedBy;
            attestation.ModifiedDate = _submitDate;
        }

        public async Task SaveJsonProviderForm(string json, int providerId, string modifiedBy)
        {
            await ModifyJsonProviderForm(json, providerId, modifiedBy);
        }

        private async Task ModifyJsonProviderForm(string json, int providerId, string modifiedBy)
        {
            // Json Provider Form

            var modifiedDate = DateTime.Now;
            if (modifiedDate == null) { throw new Exception(); };

            var existingJsonRecord = await _dbContextEntity.JsonProviderForm.Where(x => x.ProviderId == providerId).FirstOrDefaultAsync();


            if (existingJsonRecord == null)
            {
                throw new EntityNotFoundException();
            }
            else
            {

                //Update
                existingJsonRecord.ModifiedDate = modifiedDate;
                existingJsonRecord.ModifiedBy = modifiedBy;
                existingJsonRecord.JsonBody = json;
                existingJsonRecord.IsCompleted = true;
                _dbContextEntity.Entry(existingJsonRecord).State = EntityState.Modified;

            }

            JsonProviderFormHistoryEntity newProviderDraftHistory = new JsonProviderFormHistoryEntity()
            {
                ProviderId = providerId,
                JsonBody = json,
                CreatedBy = modifiedBy,
                CreationDate = modifiedDate,
                IsCompleted = true
            };

            await _dbContextEntity.JsonProviderFormHistory.AddRangeAsync(newProviderDraftHistory);

        }

        public async Task CreateCredentialingStatus(SubmitRequestDTO submitData, string modifiedBy, DateTime submitDate)
        {


            var providerId = submitData.Content.Setup.ProviderId;
            var providerInsurerCompanyStatusExists = _dbContextEntity.ProviderInsurerCompanyStatus.Where(r => r.ProviderId == providerId).Any();

            if (!providerInsurerCompanyStatusExists)
            {
                var providerPlanAcceptIds = Provider.GetProviderPlanAcceptEntities(submitData.Content.IndividualPracticeProfile.PlanAccept, providerId).Select(r => r.PlanAcceptListId);

                var InsurerCompanyIds = await _dbContextEntity.PlanAcceptList.Where(r => providerPlanAcceptIds.Contains(r.Id)).Select(r => r.InsurerCompanyId).Distinct().ToListAsync();


                foreach (var insurerCompanyId in InsurerCompanyIds)
                {

                    var porviderInsurerStatusEntity = new ProviderInsurerCompanyStatusEntity
                    {
                        InsurerStatusTypeId = StatusType.PENDING,
                        ProviderId = providerId,
                        InsurerCompanyId = insurerCompanyId,
                        SubmitDate = submitDate,
                        CurrentStatusDate = submitDate,
                        CreatedBy = modifiedBy
                    };


                    var porviderInsurerStatusHistoryEntity = new ProviderInsurerCompanyStatusHistoryEntity
                    {
                        ProviderInsurerCompanyStatusId = porviderInsurerStatusEntity.Id,
                        InsurerStatusTypeId = porviderInsurerStatusEntity.InsurerStatusTypeId,
                        StatusDate = porviderInsurerStatusEntity.CurrentStatusDate,
                        Comment = porviderInsurerStatusEntity.Comment,
                        CommentDate = porviderInsurerStatusEntity.CommentDate,
                        CreatedBy = porviderInsurerStatusEntity.CreatedBy
                    };


                    porviderInsurerStatusHistoryEntity.ProviderInsurerCompanyStatus = porviderInsurerStatusEntity;
                    await _dbContextEntity.AddAsync(porviderInsurerStatusEntity);
                    await _dbContextEntity.AddRangeAsync(porviderInsurerStatusHistoryEntity);
                }
            }
            var provider = await _dbContextEntity.Provider
                      .Where(p => p.Id == providerId)
                      .FirstOrDefaultAsync();

            if (provider == null) { throw new ProviderNotFoundException(); }
            var credForm = _dbContextEntity.CredForm.Where(p => p.Id == provider.CredFormId).FirstOrDefault();
            if (credForm == null) { throw new EntityNotFoundException(); }

            credForm.CredFormStatusTypeId = StatusType.SUBMITTED;
            credForm.SubmitDate = submitDate;
            credForm.ModifiedDate = submitDate;
            credForm.ModifiedBy = modifiedBy;

        }

        public async Task UpdateCredentialingStatus(SubmitRequestDTO submitData, string modifiedBy, DateTime submitDate)
        {
            var providerId = submitData.Content.Setup.ProviderId;
            var providerInsurerCompanyStatusIds =
                (
                    await _dbContextEntity.ProviderInsurerCompanyStatus
                    .Where
                    (
                        p => p.ProviderId == providerId &&
                        p.InsurerStatusTypeId == StatusType.RETURNED_TO_PROVIDER
                    )
                    .ToListAsync()
                )
                .Select(p => p.Id);

            _providerInsurerCompanyStatusIds = providerInsurerCompanyStatusIds.ToList();

            foreach (var providerInsurerCompanyStatusId in providerInsurerCompanyStatusIds)
            {

                var providerInsurerCompanyStatusEntity = _dbContextEntity.ProviderInsurerCompanyStatus.Find(providerInsurerCompanyStatusId);
                providerInsurerCompanyStatusEntity.InsurerStatusTypeId = StatusType.PENDING;
                providerInsurerCompanyStatusEntity.CurrentStatusDate = submitDate;
                providerInsurerCompanyStatusEntity.ModifiedBy = modifiedBy;
                providerInsurerCompanyStatusEntity.ModifiedDate = submitDate;


                var porviderInsurerStatusHistoryEntity = new ProviderInsurerCompanyStatusHistoryEntity
                {
                    ProviderInsurerCompanyStatusId = providerInsurerCompanyStatusEntity.Id,
                    InsurerStatusTypeId = providerInsurerCompanyStatusEntity.InsurerStatusTypeId,
                    StatusDate = providerInsurerCompanyStatusEntity.CurrentStatusDate,
                    Comment = providerInsurerCompanyStatusEntity.Comment,
                    CommentDate = providerInsurerCompanyStatusEntity.CommentDate,
                    CreatedBy = providerInsurerCompanyStatusEntity.ModifiedBy
                };

                await _dbContextEntity.AddAsync(porviderInsurerStatusHistoryEntity);
            }

            var provider = await _dbContextEntity.Provider
                  .Where(p => p.Id == providerId)
                  .FirstOrDefaultAsync();

            if (provider == null) { throw new ProviderNotFoundException(); }
            var credForm = await _dbContextEntity.CredForm.Where(p => p.Id == provider.CredFormId).FirstOrDefaultAsync();
            if (credForm == null) { throw new EntityNotFoundException(); }


            credForm.CredFormStatusTypeId = StatusType.RESUBMITTED;
            credForm.SubmitDate = submitDate;
            credForm.ModifiedDate = submitDate;
            credForm.ModifiedBy = modifiedBy;

        }

        public async Task Save()
        {
            await _dbContextEntity.SaveChangesAsync();
        }

        #region helpers

        private async Task AddChangeLog(string useCaseType, string createdBy, int providerId)
        {
            await _changeLogServiceBase.AddLogToContext
                (
                    createdBy,
                    useCaseType,
                    ChangeLogResourceTypes.Provider,
                    resourceId: providerId.ToString()
                );
        }

        private async Task ModifyHospitalEntities(SubmitRequestDTO submitData, int providerId)
        {
            if (submitData.Content.HospitalAffiliations != null)
            {
                await _hospitalAffiliationModificationService.Modify(submitData.Content.HospitalAffiliations, providerId);
            }
        }

        private async Task ModifyMedicalGroupEntities(SubmitRequestDTO submitData, int providerId)
        {
            if (submitData.Content.Setup.PcpApplies)
            {
                await _medicalGroupModificationService.Modify(submitData.Content.Pcp, MedicalGroupTypes.PCP, providerId);
            }

            if (submitData.Content.Setup.F330applies)
            {
                await _medicalGroupModificationService.Modify(submitData.Content.F330, MedicalGroupTypes.F330, providerId);
            }
        }

        private async Task ModifyCorporationEntities(SubmitRequestDTO submitData, int providerId)
        {
            if (!submitData.Content.IncorporatedPracticeProfile.IsNullOrEmpty())
            {
                await _incorporatedProfileResubmitService.Modify(providerId, submitData.Content.IncorporatedPracticeProfile);
            }
        }

        private async Task ModifyInsuranceEntities(SubmitRequestDTO submitData, int providerId)
        {
            if (submitData.Content.Insurance == null)
            {
                return;
            }

            await _malpracticeModificationService.Modify(submitData.Content.Insurance.Malpractice, providerId);

            if (submitData.Content.Insurance.ProfessionalLiability != null)
            {
                await _professionalLiabilityModificationService
                    .Modify(submitData.Content.Insurance.ProfessionalLiability, providerId);
            }
        }

        private async Task ModifyProviderDetailEntities(SubmitRequestDTO submitData, int providerId)
        {
            await _individualPracticeProfileResubmitService.Modify(submitData.Content.IndividualPracticeProfile, providerId);

            //Update DocumentLocation
            await ModifyDocumentLocationIndividualPracticeProfile(submitData.Content.IndividualPracticeProfile, providerId, submitData.Content.Setup.ProviderEmail);

            await _addressAndLocationResubmitService.Modify(submitData.Content.AddressAndLocation, providerId);

            await _specialtiesModificationService.Modify(submitData.Content.SpecialtiesAndSubspecialties, providerId);
        }

        private async Task ModifyEducationEntities(int providerId, EducationAndTrainingDTO educationData)
        {
            await _medicalSchoolResubmitService.Modify(providerId, educationData.MedicalSchool);

            if (!educationData.Internship.IsNullOrEmpty())
            {
                await _internshipResubmitService.Modify(providerId, educationData.Internship);
            }

            if (!educationData.Fellowship.IsNullOrEmpty())
            {
                await _fellowshipResubmitService.Modify(providerId, educationData.Fellowship);
            }

            if (!educationData.Residency.IsNullOrEmpty())
            {
                await _residencyResubmitService.Modify(providerId, educationData.Residency);
            }

            if (!educationData.BoardCertificates.IsNullOrEmpty())
            {
                await _boardResubmitService.Modify(providerId, educationData.BoardCertificates);
            }

            await _licensesModificationService.Modify(educationData.LicensesCertificates, providerId);
        }

        private async Task AddCorporationEntity(int providerId, CorporationDTO corporationDTO)
        {
            await SetCorporationDocumentIntoCorporationDto(providerId, corporationDTO);

            var (corporationEntity, publicId) = Corporation
                .GetCorporationEntityPairs(corporationDTO);

            var providerCorporation = Corporation
                .GetProviderCorporationEntity(corporationEntity, providerId, publicId);

            await _dbContextEntity.AddAsync(providerCorporation);

            if (!corporationDTO.Subspecialty.IsNullOrEmpty())
            {
                await AddCorporationSubSpecialties(corporationDTO.Subspecialty, corporationEntity);
            }

        }

        private async Task SetCorporationDocumentIntoCorporationDto(int providerId, CorporationDTO corporationDTO)
        {
            var corpNpiCertificateDto = corporationDTO.CorporateNpiCertificateFile;
            var corpCertificateDto = corporationDTO.CorporationCertificateFile;
            var corpW9Dto = corporationDTO.W9File;

            var documentLocationCorpNpiCertificate = _dbContextEntity.DocumentLocation
                                         .Where(r => r.ProviderId == providerId
                                         && r.DocumentTypeId == corpNpiCertificateDto.DocumentTypeId
                                         && r.UploadFilename == corpNpiCertificateDto.Name).FirstOrDefault();

            corporationDTO.CorporateNpiCertificateFile.AzureBlobFilename = documentLocationCorpNpiCertificate.AzureBlobFilename;


            var documentLocationcorpCertificate = _dbContextEntity.DocumentLocation
                                         .Where(r => r.ProviderId == providerId
                                         && r.DocumentTypeId == corpCertificateDto.DocumentTypeId
                                         && r.UploadFilename == corpCertificateDto.Name).FirstOrDefault();

            corporationDTO.CorporationCertificateFile.AzureBlobFilename = documentLocationcorpCertificate.AzureBlobFilename;

            if (corpW9Dto != null)
            {
                var corpW9 = await _dbContextEntity.DocumentLocation
                                             .Where(r => r.ProviderId == providerId
                                             && r.DocumentTypeId == corpW9Dto.DocumentTypeId
                                             && r.UploadFilename == corpW9Dto.Name).FirstOrDefaultAsync();

                corporationDTO.W9File.AzureBlobFilename = corpW9.AzureBlobFilename;
            }
        }

        private async Task AddCorporationSubSpecialties(int[] subSpecialties, CorporationEntity corporationEntity)
        {
            var corporationSubSpecialties = Corporation
                .GetCorporationSubspecialties(subSpecialties, corporationEntity);

            await _dbContextEntity.AddRangeAsync(corporationSubSpecialties);
        }

        private async Task AddAttestationEntities(SubmitRequestDTO submitData)
        {
            // Attestation

            var attestationEntity = Attestation.GetAttestationEntity(submitData);
            await _dbContextEntity.AddAsync(attestationEntity);
        }

        private async Task AddInsuranceEntities(SubmitRequestDTO submitData, int providerId)
        {
            // Insurance

            if (submitData.Content.Insurance.ProfessionalLiability != null)
            {
                var professionalLiabilityEntity = InsuranceHelper.GetProfessionalLiabilityEntities(submitData.Content.Insurance.ProfessionalLiability, providerId);
                await _dbContextEntity.AddAsync(professionalLiabilityEntity);

            }

            var malpracticeOIGCaseNumbers = submitData.Content.Insurance.Malpractice.OigCaseNumber
                .Select(caseNumber => new MalpracticeOIGCaseNumbers
                {
                    OIGCaseNumber = caseNumber,
                });

            var malpracticeEntity = InsuranceHelper.GetMalpracticeEntities(submitData, malpracticeOIGCaseNumbers);
            await _dbContextEntity.AddAsync(malpracticeEntity);
        }

        private async Task AddHospitalEntities(SubmitRequestDTO submitData, int providerId)
        {
            // Hospitals Affiliations

            if (submitData.Content.Setup.HospitalAffiliationsApplies)
            {
                var providerHospitalEntities = Hospital.GetProviderHospitalEntities(submitData, providerId);
                await _dbContextEntity.AddRangeAsync(providerHospitalEntities);
            }
        }


        private async Task AddEducationEntities(SubmitRequestDTO submitData, int providerId)
        {
            // Education & Training
            var medicalSchoolDtos = submitData.Content.EducationAndTraining.MedicalSchool;
            foreach (var medicalSchoolDto in medicalSchoolDtos)
            {
                var documentLocationIntership = await _documentLocationRepository.GetByProviderIdAndDocumentTypeAndUploadfilename(
                                                                providerId,
                                                                medicalSchoolDto.DiplomaFile.DocumentTypeId,
                                                                medicalSchoolDto.DiplomaFile.Name)
                    ?? throw new DocumentNotFoundException(providerId, medicalSchoolDto.DiplomaFile.DocumentTypeId, medicalSchoolDto.DiplomaFile.Name);

                medicalSchoolDto.DiplomaFile.AzureBlobFilename = documentLocationIntership?.AzureBlobFilename;
            }

            var medicalSchoolEntities = Education.GetMedicalSchoolEntities(submitData, providerId);
            _dbContextEntity.AddRange(medicalSchoolEntities);

            if (!submitData.Content.EducationAndTraining.Internship.IsNullOrEmpty())
            {

                var intershipDtos = submitData.Content.EducationAndTraining.Internship;
                foreach (var intershipDto in intershipDtos)
                {
                    var documentLocationIntership = await _documentLocationRepository.GetByProviderIdAndDocumentTypeAndUploadfilename(
                                                                    providerId,
                                                                    intershipDto.EvidenceFile.DocumentTypeId,
                                                                    intershipDto.EvidenceFile.Name)
                        ?? throw new DocumentNotFoundException(providerId, intershipDto.EvidenceFile.DocumentTypeId, intershipDto.EvidenceFile.Name);

                    intershipDto.EvidenceFile.AzureBlobFilename = documentLocationIntership?.AzureBlobFilename;
                }

                var providerInternshipEntities = Education.GetProviderInternshipEntities(submitData.Content.EducationAndTraining.Internship, providerId);
                _dbContextEntity.AddRange(providerInternshipEntities);
            }

            if (!submitData.Content.EducationAndTraining.Residency.IsNullOrEmpty())
            {
                var residencyDtos = submitData.Content.EducationAndTraining.Residency;
                foreach (var residencyDto in residencyDtos)
                {
                    var documentLocationResidency = await _documentLocationRepository.GetByProviderIdAndDocumentTypeAndUploadfilename(
                                                                    providerId,
                                                                    residencyDto.EvidenceFile.DocumentTypeId,
                                                                    residencyDto.EvidenceFile.Name)
                        ?? throw new DocumentNotFoundException(providerId, residencyDto.EvidenceFile.DocumentTypeId, residencyDto.EvidenceFile.Name);

                    residencyDto.EvidenceFile.AzureBlobFilename = documentLocationResidency?.AzureBlobFilename;
                }

                var providerResidencyEntities = Education.GetProviderResidencyEntities(submitData.Content.EducationAndTraining.Residency, providerId);
                _dbContextEntity.AddRange(providerResidencyEntities);
            }

            if (!submitData.Content.EducationAndTraining.Fellowship.IsNullOrEmpty())
            {
                var fellowshipDtos = submitData.Content.EducationAndTraining.Fellowship;
                foreach (var fellowshipDto in fellowshipDtos)
                {
                    var documentLocationFellowship = await _documentLocationRepository.GetByProviderIdAndDocumentTypeAndUploadfilename(
                                                                    providerId,
                                                                    fellowshipDto.EvidenceFile.DocumentTypeId,
                                                                    fellowshipDto.EvidenceFile.Name)
                        ?? throw new DocumentNotFoundException(providerId, fellowshipDto.EvidenceFile.DocumentTypeId, fellowshipDto.EvidenceFile.Name);

                    fellowshipDto.EvidenceFile.AzureBlobFilename = documentLocationFellowship?.AzureBlobFilename;
                }

                var providerFellowshipEntities = Education.GetProviderFellowshipEntities(submitData.Content.EducationAndTraining.Fellowship, providerId);
                _dbContextEntity.AddRange(providerFellowshipEntities);
            }

            if (!submitData.Content.EducationAndTraining.BoardCertificates.IsNullOrEmpty())
            {
                foreach (var boardDTO in submitData.Content.EducationAndTraining.BoardCertificates)
                {
                    var documentLocationBoardCertificates = await _documentLocationRepository.GetByProviderIdAndDocumentTypeAndUploadfilename(
                                                providerId,
                                                boardDTO.CertificateFile.DocumentTypeId,
                                                boardDTO.CertificateFile.Name)
                        ?? throw new DocumentNotFoundException(providerId, boardDTO.CertificateFile.DocumentTypeId, boardDTO.CertificateFile.Name);

                    boardDTO.CertificateFile.AzureBlobFilename = documentLocationBoardCertificates?.AzureBlobFilename;

                    var boardCertificate = Education.GetBoardEntity(boardDTO, providerId);
                    var boardSpecialties = Education.GetBoardSpecialtyEntities(boardDTO.SpecialtyBoard, boardCertificate);

                    _dbContextEntity.AddRange(boardSpecialties);
                }
            }

            // Licenses & Certifications

            var licencesCertificatesEntities = License.GetMedicalLicenseEntities(submitData.Content.EducationAndTraining.LicensesCertificates, providerId);
            await _dbContextEntity.AddRangeAsync(licencesCertificatesEntities);
        }

        private async Task AddMedicalGroupsEntities(SubmitRequestDTO submitData, int providerId)
        {
            // PCP & F330

            if (submitData.Content.Setup.PcpApplies)
            {
                var pcpMedicalGroupEntity = MedicalGroup.GetMedicalGroupEntity(submitData.Content.Pcp, MedicalGroupTypes.PCP);

                var providerPCPEntities = new ProviderMedicalGroupEntity
                {
                    ProviderId = providerId,
                    MedicalGroup = pcpMedicalGroupEntity,
                };

                await _dbContextEntity.AddRangeAsync(providerPCPEntities);
            }

            if (submitData.Content.Setup.F330applies)
            {
                var f330MedicalGroupEntity = MedicalGroup.GetMedicalGroupEntity(submitData.Content.F330, MedicalGroupTypes.F330);

                var providerF330Entities = new ProviderMedicalGroupEntity
                {
                    ProviderId = providerId,
                    MedicalGroup = f330MedicalGroupEntity,
                };

                await _dbContextEntity.AddRangeAsync(providerF330Entities);
            }
        }

        private async Task AddCorporationEntities(SubmitRequestDTO submitData, int providerId)
        {
            var corporationDTOs = submitData.Content.IncorporatedPracticeProfile;

            foreach (var corporationDTO in corporationDTOs)
            {
                await AddCorporationEntity(providerId, corporationDTO);
            }
        }

        private async Task AddProviderProfileEntities(SubmitRequestDTO submitData, int providerId)
        {
            // Individual Practice Profiles

            var providerDetailEntities = Provider.GetProviderDetailEntity(submitData.Content.IndividualPracticeProfile, providerId);
            var providerPlanAcceptEntities = Provider.GetProviderPlanAcceptEntities(submitData.Content.IndividualPracticeProfile.PlanAccept, providerId);
            var providerAddresses = submitData.Content.AddressAndLocation
                .SelectMany(address => Provider.GetProviderAddressEntities(address, providerId))
                .ToList();

            //Update DocumentLocation
            ModifyDocumentLocationIndividualPracticeProfile(submitData.Content.IndividualPracticeProfile, providerId, submitData.Content.Setup.ProviderEmail);

            await _dbContextEntity.AddAsync(providerDetailEntities);
            await _dbContextEntity.AddRangeAsync(providerPlanAcceptEntities);
            await _dbContextEntity.AddRangeAsync(providerAddresses);

            var providerSpecialtyDocuments = await GetDocumentsByType(providerId, DocumentTypes.Specialty);

            if (!providerSpecialtyDocuments.IsNullOrEmpty())
            {
                var providerSpecialties = Specialties.GetProviderSpecialtyEntities(submitData.Content.SpecialtiesAndSubspecialties.Specialties, providerSpecialtyDocuments, providerId);
                await _dbContextEntity.AddRangeAsync(providerSpecialties);
            }

            var providerSubSpecialtyDocuments = await GetDocumentsByType(providerId, DocumentTypes.SubSpecialty);

            if (!providerSubSpecialtyDocuments.IsNullOrEmpty())
            {
                var providerSubspecialties = Specialties.GetProviderSubSpecialtyEntities(submitData.Content.SpecialtiesAndSubspecialties.Subspecialties, providerSubSpecialtyDocuments, providerId);
                await _dbContextEntity.AddRangeAsync(providerSubspecialties);
            }
        }

        private async Task ModifyDocumentLocationIndividualPracticeProfile(IndividualPracticeProfileDTO individualPracticeProfileDTO, int providerId, string by)
        {
            var modifiedDate = DateTime.Now;
            List<int> sectionIds = new List<int> { 1, 2, 3 };
            var documentLocationEntities = await _dbContextEntity.DocumentLocation
                            .Where(r => r.ProviderId == providerId && r.IsActive == true)
                            .Include(r => r.DocumentType.DocumentSectionType)
                            .Where(r => sectionIds.Contains(r.DocumentType.DocumentSectionTypeId))
                            .ToListAsync();


            var documentLocationNpi = documentLocationEntities.Where(r => r.DocumentTypeId == 1).FirstOrDefault();
            if (documentLocationNpi == null) { throw new DocumentNotFoundException(); };

            documentLocationNpi.NPI = individualPracticeProfileDTO.NpiCertificateNumber;
            documentLocationNpi.ModifiedBy = by;
            documentLocationNpi.ModifiedDate = modifiedDate;
            _dbContextEntity.Entry(documentLocationNpi).State = EntityState.Modified;

            List<int> documentTypeIds = new List<int> { 4, 5, 6, 7, 8, 9 };
            var documentLocationId = documentLocationEntities.Where(r => documentTypeIds.Contains(r.DocumentTypeId)).FirstOrDefault();
            if (documentLocationId == null) { throw new DocumentNotFoundException(); };

            documentLocationId.ExpirationDate = DateTimeHelper.ParseDate(individualPracticeProfileDTO.IdExpDate);
            documentLocationId.ModifiedBy = by;
            documentLocationId.ModifiedDate = modifiedDate;
            _dbContextEntity.Entry(documentLocationNpi).State = EntityState.Modified;

        }

        private async Task ModifyDocumentLocationCriminalRecord(CriminalRecordDTO criminalRecordDTO, int providerId, string by)
        {
            var modifiedDate = DateTime.Now;

            var documentLocationEntity = await _dbContextEntity.DocumentLocation
                            .Where(r => r.ProviderId == providerId && r.IsActive == true && r.DocumentTypeId == 21)
                            .FirstOrDefaultAsync();

            documentLocationEntity.ExpirationDate = DateTimeHelper.ParseDate(criminalRecordDTO.NegativePenalRecordExpDate);
            documentLocationEntity.IssueDate = DateTimeHelper.ParseDate(criminalRecordDTO.NegativePenalRecordIssuedDate);
            documentLocationEntity.ModifiedBy = by;
            documentLocationEntity.ModifiedDate = modifiedDate;
            _dbContextEntity.Entry(documentLocationEntity).State = EntityState.Modified;

        }

        private async Task<IEnumerable<DocumentLocationEntity>> GetDocumentsByType(int providerId, int documentType)
        {
            return await _dbContextEntity.DocumentLocation
                 .Where(dl => dl.ProviderId == providerId)
                 .Where(dl => dl.DocumentTypeId == documentType)
                 .ToListAsync();
        }

        private void SetCreatedByColumns(string submitterEmail)
        {
            var recordHistories = _dbContextEntity.ChangeTracker.Entries<EntityAuditBase>();
            foreach (var recordHistory in recordHistories)
            {
                if (recordHistory.State == EntityState.Added || recordHistory.Entity.CreatedBy == null)
                {
                    recordHistory.Entity.CreatedBy = submitterEmail;
                    recordHistory.Entity.CreationDate = DateTime.Now;
                }
            }
        }

        private void SetModifiedByColumns(string submitterEmail)
        {
            // TODO: modification fields are being set for entites with unmodified data.

            var recordHistories = _dbContextEntity.ChangeTracker.Entries<EntityAuditBase>();

            foreach (var recordHistory in recordHistories)
            {
                if (recordHistory.State == EntityState.Added || recordHistory.Entity.CreatedBy == null)
                {
                    recordHistory.Entity.CreatedBy = submitterEmail;
                    recordHistory.Entity.CreationDate = DateTime.Now;
                }

                if (recordHistory.State == EntityState.Modified || recordHistory.Entity.ModifiedBy == null)
                {
                    recordHistory.Entity.ModifiedBy = submitterEmail;
                    recordHistory.Entity.ModifiedDate = DateTime.Now;
                }
            }
        }

        #endregion
    }
}
