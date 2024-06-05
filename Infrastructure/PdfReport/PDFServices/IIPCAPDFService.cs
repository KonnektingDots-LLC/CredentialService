using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.DTO.Documents;
using cred_system_back_end_app.Domain.Interfaces.pdf;
using cred_system_back_end_app.Infrastructure.AzureBlobStorage;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using cred_system_back_end_app.Infrastructure.Data.Repositories;
using cred_system_back_end_app.Infrastructure.PdfReport.CredentialingApplication;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Infrastructure.PdfReport.PDFServices
{
    /// <inheritdoc/>
    public class IIPCAPDFService : PDFServiceBase<IIPCAPdfRootDto>, IIipcaPdfService
    {
        private readonly Func<string, string, string>
            _getIIPCAFormFileName = GetIIPCAFormFilename;

        private readonly PDFDataRepository _pdfDataRepository;
        private readonly DbContextEntity _dbContextEntity;

        /// <inheritdoc/>
        public IIPCAPDFService(
            PdfGeneratorClient<IIPCAPdfRootDto> pdfGeneratorClient,
            DocumentUploadService documentCase,
            PDFDataRepository pdfDataRepository,
            DbContextEntity dbContextEntity)
            : base(pdfGeneratorClient, documentCase, dbContextEntity)
        {
            DocumentType = DocumentTypes.IIPCA_FORM;

            PDFGeneratorApiSuffix = "IIPCAPdfHttpTrigger";

            _pdfDataRepository = pdfDataRepository;
            _dbContextEntity = dbContextEntity;
        }

        /// <summary>
        /// Generates IIPCA PDF from Submit data and uploads it to storage.
        /// </summary>
        /// <param name="submitDTO"></param>
        /// <returns></returns>
        public async Task<PdfDocumentResponse> HandlePDF(SubmitRequestDTO submitDTO, DateTime submitDate)
        {
            var providerId = submitDTO.Content.Setup.ProviderId;

            ProviderId = providerId;
            UploadFileName = _getIIPCAFormFileName(providerId.ToString(), submitDate.ToString());
            UploadBy = submitDTO.Content.Setup.ProviderEmail;
            UploadDate = submitDate;

            var pdfRequestDTO = await GetPdfDTO(submitDTO);

            return await HandlePDF(pdfRequestDTO);
        }

        static string GetIIPCAFormFilename(string providerId, string submitDate)
        {
            return providerId + "_IIPCA_Form_" + submitDate + ".pdf";
        }

        private async Task<IIPCAPdfRootDto> GetPdfDTO(SubmitRequestDTO submitDTO)
        {
            var setupDTO = submitDTO.Content.Setup;
            var providerId = setupDTO.ProviderId;

            _pdfDataRepository.SetDbContextTransaction(_dbContextEntity);

            var pdfDTO = new IIPCAPdfRootDto
            {
                FormSections = new FormSectionsDto
                {
                    //TODO: these should be cases, not repos.

                    IndPrimaryPracticeProfile1 = _pdfDataRepository.GetIndividualPracticeProfile(providerId),
                    EducationAndTraining = _pdfDataRepository.GetEducationAndTrainingDto(providerId),
                    LicenseAndCertification = _pdfDataRepository.GetLicenseAndCertificationDto(providerId),
                    NegativeCertificatePenalRecordDate = _pdfDataRepository.GetNegeativePenalRecordCertificate(providerId),
                    AdditionalDirectory = _pdfDataRepository.GetAdditionalProviderData(),
                }
            };

            var formSections = pdfDTO.FormSections;

            if (!submitDTO.Content.IncorporatedPracticeProfile.IsNullOrEmpty())
            {
                var (primaryCorp, additionalCorps) = _pdfDataRepository.GetCorporatePracticeProfile(providerId);

                formSections.CorporatePracticeProfile2 = primaryCorp;
                formSections.AdditionalCorporatePracticeProfile = additionalCorps;
            }

            if (setupDTO.PcpApplies)
            {
                formSections.PrimaryCarePhysicianPCP = await _pdfDataRepository.GetPCPProfile(providerId);
            }

            if (setupDTO.F330applies)
            {
                formSections.FederalQualifiedHealthCenter330 = _pdfDataRepository.GetF330Profile(providerId);
            }

            if (setupDTO.HospitalAffiliationsApplies)
            {
                formSections.HospitalAffiliations = _pdfDataRepository.GetHospitalAffiliations(providerId);
            }

            if (setupDTO.InsuranceApplies)
            {
                formSections.Malpractice = _pdfDataRepository.GetMalpracticeData(providerId);
                formSections.ProfessionalLiability = _pdfDataRepository.GetProfessionalLiabilityData(providerId);
            }

            return pdfDTO;
        }
    }
}
