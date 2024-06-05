using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.DTO.Documents;
using cred_system_back_end_app.Domain.Interfaces.pdf;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.AzureBlobStorage;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using cred_system_back_end_app.Infrastructure.PdfReport.CredentialingApplication;

namespace cred_system_back_end_app.Infrastructure.PdfReport.PDFServices.AttestationPDF
{
    /// <inheritdoc/>
    public class AttestationPDFService : PDFServiceBase<AttestationPDFRequestDTO>, IAttestationPdfService
    {
        private readonly Func<string, string, string> _getAttestationFileName = GetAttestationFilename;
        private readonly IAttestationRepository _attestationRepository;

        /// <inheritdoc/>
        public AttestationPDFService
        (
            IAttestationRepository attestationRepository,
            PdfGeneratorClient<AttestationPDFRequestDTO> pdfGeneratorClient,
            DocumentUploadService documentCase,
            DbContextEntity dbContextEntity
        )
            : base(pdfGeneratorClient, documentCase, dbContextEntity)
        {
            _attestationRepository = attestationRepository;

            DocumentType = DocumentTypes.ATTESTATION;
            PDFGeneratorApiSuffix = "AttestationPdfHttpTrigger";
        }

        public async Task<PdfDocumentResponse> HandlePDF(SubmitRequestDTO submitDTO, DateTime submitDate)
        {
            var providerId = submitDTO.Content.Setup.ProviderId;

            ProviderId = providerId;
            UploadFileName = _getAttestationFileName(providerId.ToString(), submitDate.ToString());
            UploadBy = submitDTO.Content.Setup.ProviderEmail;
            UploadDate = submitDate;

            var attestationRequestDTO = await GetAttestationDTO(providerId);//submitDTO

            return await HandlePDF(attestationRequestDTO);
        }

        static string GetAttestationFilename(string providerId, string submitDate)
        {
            return providerId + "_Attestation_" + submitDate + ".pdf";
        }

        private async Task<AttestationPDFRequestDTO> GetAttestationDTO(int providerId)//submitDTO
        {
            var attestationEntity = await _attestationRepository.GetAttestationByProviderIdAsync(providerId);

            //realizar el Attestation con submitDTO

            var attestationRequestDTO = new AttestationPDFRequestDTO
            {
                Name = attestationEntity.Provider.GetFullName(),
                Date = attestationEntity.AttestationDate.ToString("dddd, MMM. d, yyyy"),
            };

            return attestationRequestDTO;
        }
    }
}
