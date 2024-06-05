using AutoMapper;
using cred_system_back_end_app.Application.Common.Constant;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionList;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.Common.Mappers.DTOToEntity;
using cred_system_back_end_app.Application.CRUD.ChangeLogservices;
using cred_system_back_end_app.Application.CRUD.CredForm;
using cred_system_back_end_app.Application.CRUD.Document;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatus.DTO;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatusHistory.DTO;
using cred_system_back_end_app.Application.UseCase.ProviderInsurerCompanyStatus;
using cred_system_back_end_app.Application.UseCase.Submit.DTO;
using cred_system_back_end_app.Application.UseCase.Submit.DTO.EducationDTOs;
using cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices;
using cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices.EducationResubmitServices;
using cred_system_back_end_app.Infrastructure.DB;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Application.UseCase.Submit
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
        private readonly ChangeLogRepository _changeLogServiceBase;
        private readonly CredFormRepository _credFormRepository;
        private readonly ProviderInsurerCompanyStatusUseCase _providerInsurerCompanyStatusUseCase;
        private readonly IMapper _mapper;
        private readonly DocumentCase _documentCase;
        private DateTime _submitDate;
        private List<int> _providerInsurerCompanyStatusIds;

        public SubmitDBContextManager (
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
            ChangeLogRepository changeLogServiceBase,
            CredFormRepository credFormRepository,
            ProviderInsurerCompanyStatusUseCase providerInsurerCompanyStatusUseCase,
            IMapper mapper,
            DocumentCase documentCase
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
            _credFormRepository = credFormRepository;
            _providerInsurerCompanyStatusUseCase = providerInsurerCompanyStatusUseCase;
            _mapper = mapper;
            _documentCase = documentCase;
        }

        public DbContextEntity GetDbContextTransaction()
        {
            return _dbContextEntity;
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

            AddProviderProfileEntities(submitData, providerId);

            if (!submitData.Content.IncorporatedPracticeProfile.IsNullOrEmpty())
            {
                await AddCorporationEntities(submitData, providerId);
            }

            AddMedicalGroupsEntities(submitData, providerId);

            AddEducationEntities(submitData, providerId);

            if (submitData.Content.Setup.HospitalAffiliationsApplies)
            {
                AddHospitalEntities(submitData, providerId);
            }

            ModifyDocumentLocationCriminalRecord(submitData.Content.CriminalRecord,providerId, createdBy);

            if (submitData.Content.Setup.InsuranceApplies)
            {
                AddInsuranceEntities(submitData, providerId);
            }

            AddAttestationEntities(submitData);

            SetCreatedByColumns(createdBy);

           // await UpdateJsonProviderFormTables(providerId);          

            AddChangeLog(ChangeLogUseCases.Submit, createdBy, providerId);
        }

        public async Task ModifyEntities(SubmitRequestDTO submitData, string modifiedBy)
        {
            var providerId = submitData.Content.Setup.ProviderId;

            await ModifyProviderDetailEntities(submitData, providerId);

            await ModifyCorporationEntities(submitData, providerId);

            await ModifyMedicalGroupEntities(submitData, providerId);

            await ModifyHospitalEntities(submitData, providerId);

            await ModifyEducationEntities(providerId, submitData.Content.EducationAndTraining);

            ModifyDocumentLocationCriminalRecord(submitData.Content.CriminalRecord, providerId, modifiedBy);

            await ModifyInsuranceEntities(submitData, providerId);

            ModifyAttestation(submitData.Content.Attestation.AttestationDate, providerId,modifiedBy);

            SetModifiedByColumns(modifiedBy);

            AddChangeLog(ChangeLogUseCases.ReSubmit, modifiedBy, providerId);
        }

        private void ModifyAttestation(string AttestationDate, int providerId, string modifiedBy)
        {
            var attestation = _dbContextEntity.Attestation.Where(r => r.ProviderId == providerId).FirstOrDefault();
            attestation.AttestationDate = DateTimeHelper.ParseDate(AttestationDate);
            attestation.ModifiedBy = modifiedBy;
            attestation.ModifiedDate = _submitDate;
        }

        public async Task SaveJsonProviderForm(string json,int providerId, string modifiedBy)
        {
            await ModifyJsonProviderForm(json,providerId, modifiedBy);
        }

        private async Task ModifyJsonProviderForm(string json,int providerId, string modifiedBy)
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
                var providerPlanAcceptIds = Common.Mappers.DTOToEntity.Provider.GetProviderPlanAcceptEntities(submitData.Content.IndividualPracticeProfile.PlanAccept, providerId).Select(r => r.PlanAcceptListId);

                var InsurerCompanyIds = _dbContextEntity.PlanAcceptList.Where(r => providerPlanAcceptIds.Contains(r.Id)).Select(r => r.InsurerCompanyId).Distinct().ToList();


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
                    _dbContextEntity.Add(porviderInsurerStatusEntity);
                    _dbContextEntity.AddRange(porviderInsurerStatusHistoryEntity);
                }             

                //await _dbContextEntity.SaveChangesAsync();

            }
            var provider = _dbContextEntity.Provider
                      .Where(p => p.Id == providerId)
                      .FirstOrDefault();

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


                //await _providerInsurerCompanyStatusUseCase.UpdatePICSAndCreatePICSH(porviderInsurerStatusDTO);
                var porviderInsurerStatusHistoryEntity = new ProviderInsurerCompanyStatusHistoryEntity
                {
                    ProviderInsurerCompanyStatusId = providerInsurerCompanyStatusEntity.Id,
                    InsurerStatusTypeId = providerInsurerCompanyStatusEntity.InsurerStatusTypeId,
                    StatusDate = providerInsurerCompanyStatusEntity.CurrentStatusDate,
                    Comment = providerInsurerCompanyStatusEntity.Comment,
                    CommentDate = providerInsurerCompanyStatusEntity.CommentDate,
                    CreatedBy = providerInsurerCompanyStatusEntity.ModifiedBy
                };

                _dbContextEntity.Add(porviderInsurerStatusHistoryEntity);
            }

            var provider = _dbContextEntity.Provider
                  .Where(p => p.Id == providerId)
                  .FirstOrDefault();

            if (provider == null) { throw new ProviderNotFoundException(); }
            var credForm = _dbContextEntity.CredForm.Where(p => p.Id == provider.CredFormId).FirstOrDefault();
            if (credForm == null) { throw new EntityNotFoundException(); }


            credForm.CredFormStatusTypeId = StatusType.RESUBMITTED;
            credForm.SubmitDate = submitDate;
            credForm.ModifiedDate = submitDate;
            credForm.ModifiedBy = modifiedBy;

        }

        public async Task SaveAndCommit() 
        {
            using (var dbTransaction = _dbContextEntity.Database.BeginTransaction())
            {
                await _dbContextEntity.SaveChangesAsync();
                await dbTransaction.CommitAsync();
            }
        }

        public void Save()
        {
            _dbContextEntity.SaveChanges();
        }

        public IDbContextTransaction DBBeginTransaction()
        {
            return _dbContextEntity.Database.BeginTransaction();
        }

        public void Commit(IDbContextTransaction dbContextTransaction)
        {
            dbContextTransaction.Commit();
        }

        #region helpers

        private void AddChangeLog(string useCaseType, string createdBy, int providerId)
        {
            _changeLogServiceBase.AddLogToContext
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
            ModifyDocumentLocationIndividualPracticeProfile(submitData.Content.IndividualPracticeProfile, providerId, submitData.Content.Setup.ProviderEmail);

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

        private void AddCorporationEntity(int providerId, CorporationDTO corporationDTO)
        {
            SetCorporationDocumentIntoCorporationDto(providerId, corporationDTO);

            var (corporationEntity, publicId) = Common.Mappers.DTOToEntity.Corporation
                .GetCorporationEntityPairs(corporationDTO);

            var providerCorporation = Common.Mappers.DTOToEntity.Corporation
                .GetProviderCorporationEntity(corporationEntity, providerId, publicId);

            _dbContextEntity.Add(providerCorporation);

            if (!corporationDTO.Subspecialty.IsNullOrEmpty())
            {
                AddCorporationSubSpecialties(corporationDTO.Subspecialty, corporationEntity);
            }

        }

        private CorporationDTO SetCorporationDocumentIntoCorporationDto(int providerId, CorporationDTO corporationDTO)
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

            if(corpW9Dto != null) {
                var corpW9 = _dbContextEntity.DocumentLocation
                                             .Where(r => r.ProviderId == providerId
                                             && r.DocumentTypeId == corpW9Dto.DocumentTypeId
                                             && r.UploadFilename == corpW9Dto.Name).FirstOrDefault();

                corporationDTO.W9File.AzureBlobFilename = corpW9.AzureBlobFilename;
            }


            return corporationDTO;

        }
        private void AddCorporationSubSpecialties(int[] subSpecialties, CorporationEntity corporationEntity)
        {
            var corporationSubSpecialties = Common.Mappers.DTOToEntity.Corporation
                .GetCorporationSubspecialties(subSpecialties, corporationEntity);

            _dbContextEntity.AddRange(corporationSubSpecialties);
        }

        private void AddAttestationEntities(SubmitRequestDTO submitData)
        {
            // Attestation

            var attestationEntity = Attestation.GetAttestationEntity(submitData);
            _dbContextEntity.Add(attestationEntity);
        }

        private void AddInsuranceEntities(SubmitRequestDTO submitData, int providerId)
        {
            // Insurance

            if (submitData.Content.Insurance.ProfessionalLiability != null)
            {
                var professionalLiabilityEntity = InsuranceHelper.GetProfessionalLiabilityEntities(submitData.Content.Insurance.ProfessionalLiability, providerId);
                _dbContextEntity.Add(professionalLiabilityEntity);

            }

            var malpracticeOIGCaseNumbers = submitData.Content.Insurance.Malpractice.OigCaseNumber
                .Select(caseNumber => new MalpracticeOIGCaseNumbers
                {
                    OIGCaseNumber = caseNumber,
                });

            var malpracticeEntity = InsuranceHelper.GetMalpracticeEntities(submitData, malpracticeOIGCaseNumbers);
            _dbContextEntity.Add(malpracticeEntity);
        }

        private void AddHospitalEntities(SubmitRequestDTO submitData, int providerId)
        {
            // Hospitals Affiliations

            if (submitData.Content.Setup.HospitalAffiliationsApplies)
            {
                var providerHospitalEntities = Hospital.GetProviderHospitalEntities(submitData, providerId);
                _dbContextEntity.AddRange(providerHospitalEntities);
            }
        }


        private void AddEducationEntities(SubmitRequestDTO submitData, int providerId)
        {
            // Education & Training
            var medicalSchoolDtos = submitData.Content.EducationAndTraining.MedicalSchool;
            foreach (var medicalSchoolDto in medicalSchoolDtos)
            {
                var documentLocationIntership = _documentCase.GetDocumentLocationEntityByProviderIdDocTypeFilename(
                                                                providerId,
                                                                medicalSchoolDto.DiplomaFile.DocumentTypeId,
                                                                medicalSchoolDto.DiplomaFile.Name);

                medicalSchoolDto.DiplomaFile.AzureBlobFilename = documentLocationIntership?.AzureBlobFilename;
            }

            var medicalSchoolEntities = Education.GetMedicalSchoolEntities(submitData, providerId);
            _dbContextEntity.AddRange(medicalSchoolEntities);

            if (!submitData.Content.EducationAndTraining.Internship.IsNullOrEmpty())
            {

                var intershipDtos = submitData.Content.EducationAndTraining.Internship;
                foreach (var intershipDto in intershipDtos)
                {
                    var documentLocationIntership = _documentCase.GetDocumentLocationEntityByProviderIdDocTypeFilename(
                                                                    providerId,
                                                                    intershipDto.EvidenceFile.DocumentTypeId,
                                                                    intershipDto.EvidenceFile.Name);

                    intershipDto.EvidenceFile.AzureBlobFilename = documentLocationIntership?.AzureBlobFilename;
                }

                var providerInternshipEntities = Education.GetProviderInternshipEntities(submitData.Content.EducationAndTraining.Internship, providerId);
                _dbContextEntity.AddRange(providerInternshipEntities);

                //var EducationInfoDocumentIntershipEntities = Education.GetEducationInfoDocumentIntershipEntities(submitData.Content.EducationAndTraining.Internship);
                //_dbContextEntity.AddRange(EducationInfoDocumentIntershipEntities);

            }

            if (!submitData.Content.EducationAndTraining.Residency.IsNullOrEmpty())
            {
                var residencyDtos = submitData.Content.EducationAndTraining.Residency;
                foreach (var residencyDto in residencyDtos)
                {
                    var documentLocationResidency = _documentCase.GetDocumentLocationEntityByProviderIdDocTypeFilename(
                                                                    providerId,
                                                                    residencyDto.EvidenceFile.DocumentTypeId,
                                                                    residencyDto.EvidenceFile.Name);

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
                    var documentLocationFellowship = _documentCase.GetDocumentLocationEntityByProviderIdDocTypeFilename(
                                                                    providerId,
                                                                    fellowshipDto.EvidenceFile.DocumentTypeId,
                                                                    fellowshipDto.EvidenceFile.Name);

                    fellowshipDto.EvidenceFile.AzureBlobFilename = documentLocationFellowship?.AzureBlobFilename;
                }

                var providerFellowshipEntities = Education.GetProviderFellowshipEntities(submitData.Content.EducationAndTraining.Fellowship, providerId);
                _dbContextEntity.AddRange(providerFellowshipEntities);
            }

            if (!submitData.Content.EducationAndTraining.BoardCertificates.IsNullOrEmpty())
            {
                foreach (var boardDTO in submitData.Content.EducationAndTraining.BoardCertificates)
                {
                    var documentLocationBoardCertificates = _documentCase.GetDocumentLocationEntityByProviderIdDocTypeFilename(
                                                providerId,
                                                boardDTO.CertificateFile.DocumentTypeId,
                                                boardDTO.CertificateFile.Name);

                    boardDTO.CertificateFile.AzureBlobFilename = documentLocationBoardCertificates?.AzureBlobFilename;

                    var boardCertificate = Education.GetBoardEntity(boardDTO, providerId);
                    var boardSpecialties = Education.GetBoardSpecialtyEntities(boardDTO.SpecialtyBoard, boardCertificate);

                    _dbContextEntity.AddRange(boardSpecialties);
                }
            }

            // Licenses & Certifications

            var licencesCertificatesEntities = License.GetMedicalLicenseEntities(submitData.Content.EducationAndTraining.LicensesCertificates, providerId);
            _dbContextEntity.AddRange(licencesCertificatesEntities);
        }

        private void AddMedicalGroupsEntities(SubmitRequestDTO submitData, int providerId)
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

                _dbContextEntity.AddRange(providerPCPEntities);
            }

            if (submitData.Content.Setup.F330applies)
            {
                var f330MedicalGroupEntity = MedicalGroup.GetMedicalGroupEntity(submitData.Content.F330, MedicalGroupTypes.F330);

                var providerF330Entities = new ProviderMedicalGroupEntity
                {
                    ProviderId = providerId,
                    MedicalGroup = f330MedicalGroupEntity,
                };

                _dbContextEntity.AddRange(providerF330Entities);
            }
        }

        private async Task AddCorporationEntities(SubmitRequestDTO submitData, int providerId)
        {
            var corporationDTOs = submitData.Content.IncorporatedPracticeProfile;

            foreach (var corporationDTO in corporationDTOs)
            {
                AddCorporationEntity(providerId, corporationDTO);
            }

            return;
        }

        private async void AddProviderProfileEntities(SubmitRequestDTO submitData, int providerId)
        {
            // Individual Practice Profiles

            var providerDetailEntities = Common.Mappers.DTOToEntity.Provider.GetProviderDetailEntity(submitData.Content.IndividualPracticeProfile, providerId);
            var providerPlanAcceptEntities = Common.Mappers.DTOToEntity.Provider.GetProviderPlanAcceptEntities(submitData.Content.IndividualPracticeProfile.PlanAccept, providerId);
            var providerAddresses = submitData.Content.AddressAndLocation
                .SelectMany(address => Common.Mappers.DTOToEntity.Provider.GetProviderAddressEntities(address, providerId))
                .ToList();

            //Update DocumentLocation
             ModifyDocumentLocationIndividualPracticeProfile(submitData.Content.IndividualPracticeProfile, providerId, submitData.Content.Setup.ProviderEmail);

            _dbContextEntity.Add(providerDetailEntities);
            _dbContextEntity.AddRange(providerPlanAcceptEntities);
            _dbContextEntity.AddRange(providerAddresses);

            var providerSpecialtyDocuments = GetDocumentsByType(providerId, DocumentTypes.Specialty);

            if (!providerSpecialtyDocuments.IsNullOrEmpty())
            {
                var providerSpecialties = Specialties.GetProviderSpecialtyEntities(submitData.Content.SpecialtiesAndSubspecialties.Specialties, providerSpecialtyDocuments, providerId);
                _dbContextEntity.AddRange(providerSpecialties);
            }

            var providerSubSpecialtyDocuments = GetDocumentsByType(providerId, DocumentTypes.SubSpecialty);

            if (!providerSubSpecialtyDocuments.IsNullOrEmpty())
            {
                var providerSubspecialties = Specialties.GetProviderSubSpecialtyEntities(submitData.Content.SpecialtiesAndSubspecialties.Subspecialties, providerSubSpecialtyDocuments, providerId);
                _dbContextEntity.AddRange(providerSubspecialties);
            }
        }

        private void ModifyDocumentLocationIndividualPracticeProfile(IndividualPracticeProfileDTO individualPracticeProfileDTO, int providerId, string by)
        {
            var modifiedDate = DateTime.Now;
            List<int> sectionIds = new List<int> { 1,2,3 };
            var documentLocationEntities =  _dbContextEntity.DocumentLocation
                            .Where(r => r.ProviderId == providerId && r.IsActive == true)
                            .Include(r => r.DocumentType.DocumentSectionType)
                            .Where(r => sectionIds.Contains(r.DocumentType.DocumentSectionTypeId))
                            .ToList();


            var documentLocationNpi = documentLocationEntities.Where(r => r.DocumentTypeId == 1).FirstOrDefault();
            if (documentLocationNpi == null) { throw new DocumentNotFoundException(); };

            documentLocationNpi.NPI = individualPracticeProfileDTO.NpiCertificateNumber;
            documentLocationNpi.ModifiedBy = by;
            documentLocationNpi.ModifiedDate = modifiedDate;
            _dbContextEntity.Entry(documentLocationNpi).State = EntityState.Modified;

            List<int> documentTypeIds = new List<int> { 4, 5, 6,7,8,9 };
            var documentLocationId = documentLocationEntities.Where(r => documentTypeIds.Contains(r.DocumentTypeId)).FirstOrDefault();
            if (documentLocationId == null) { throw new DocumentNotFoundException(); };

            documentLocationId.ExpirationDate = DateTimeHelper.ParseDate(individualPracticeProfileDTO.IdExpDate);
            documentLocationId.ModifiedBy = by;
            documentLocationId.ModifiedDate = modifiedDate;
            _dbContextEntity.Entry(documentLocationNpi).State = EntityState.Modified;

        }

        private void ModifyDocumentLocationCriminalRecord(CriminalRecordDTO criminalRecordDTO, int providerId, string by)
        {
            var modifiedDate = DateTime.Now;

            var documentLocationEntity = _dbContextEntity.DocumentLocation
                            .Where(r => r.ProviderId == providerId && r.IsActive == true && r.DocumentTypeId == 21)
                            .FirstOrDefault();

            documentLocationEntity.ExpirationDate = DateTimeHelper.ParseDate(criminalRecordDTO.NegativePenalRecordExpDate);
            documentLocationEntity.IssueDate = DateTimeHelper.ParseDate(criminalRecordDTO.NegativePenalRecordIssuedDate);
            documentLocationEntity.ModifiedBy = by;
            documentLocationEntity.ModifiedDate = modifiedDate;
            _dbContextEntity.Entry(documentLocationEntity).State = EntityState.Modified;

        }

        private async Task UpdateJsonProviderFormTables(int providerId)
        {
            var jsonProviderForm = await _dbContextEntity.JsonProviderForm
                .Where(j => j.ProviderId == providerId)
                .FirstOrDefaultAsync();

            var jsonProviderFormHistoryLastEntry = await _dbContextEntity.JsonProviderFormHistory
                .Where(j => j.ProviderId == providerId)
                .OrderBy(j => j.CreationDate)
                .LastAsync();

            jsonProviderForm.IsCompleted = true;
            jsonProviderFormHistoryLastEntry.IsCompleted = true;
        }

        private IEnumerable<DocumentLocationEntity> GetDocumentsByType(int providerId, int documentType)
        {
            return _dbContextEntity.DocumentLocation
                 .Where(dl => dl.ProviderId == providerId)
                 .Where(dl => dl.DocumentTypeId == documentType)
                 .ToList();
        }

        private void SetCreatedByColumns(string submitterEmail)
        {
            var recordHistories = _dbContextEntity.ChangeTracker.Entries<RecordHistory>();

            foreach (var recordHistory in recordHistories)
            {
                if (recordHistory.State == EntityState.Added)
                {
                    recordHistory.Entity.CreatedBy = submitterEmail;
                    recordHistory.Entity.CreationDate = DateTime.Now;
                }
            }
        }

        private void SetModifiedByColumns(string submitterEmail)
        {
            // TODO: modification fields are being set for entites with unmodified data.

            var recordHistories = _dbContextEntity.ChangeTracker.Entries<RecordHistory>();

            foreach (var recordHistory in recordHistories)
            {
                if (recordHistory.State == EntityState.Added)
                {
                    recordHistory.Entity.CreatedBy = submitterEmail;
                    recordHistory.Entity.CreationDate = DateTime.Now;
                }

                if (recordHistory.State == EntityState.Modified)
                {
                    recordHistory.Entity.ModifiedBy = submitterEmail;
                    recordHistory.Entity.ModifiedDate = DateTime.Now;
                }
            }
        }

        #endregion
    }
}
