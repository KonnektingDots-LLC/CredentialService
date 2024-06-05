using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.CRUD.Document;
using cred_system_back_end_app.Application.CRUD.Document.DTO;
using cred_system_back_end_app.Application.CRUD.IIPCAFormSections;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.PdfReport.CredentialingApplication;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.Common.Services.PDFServices.AttestationPDF
{
    /// <inheritdoc/>
    public class AttestationPDFService : PDFServiceBase<AttestationPDFRequestDTO>
    {
        private readonly Func<string,string, string> _getAttestationFileName = GetAttestationFilename;
        private readonly AttestationRepository _attestationRepository;
        private DbContextEntity _dbContextEntity;

        /// <inheritdoc/>
        public AttestationPDFService
        (
            AttestationRepository attestationRepository,
            PdfGeneratorClient<AttestationPDFRequestDTO> pdfGeneratorClient, 
            DocumentCase documentCase,
            DbContextEntity dbContextEntity
        ) 
            : base(pdfGeneratorClient, documentCase, dbContextEntity) 
        {
            _attestationRepository = attestationRepository;

            DocumentType = DocumentTypes.ATTESTATION;
            PDFGeneratorApiSuffix = "AttestationPdfHttpTrigger";
            _dbContextEntity = dbContextEntity;
        }

        //public void SetDbContextTransaction( DbContextEntity dbContextEntity)
        //{
        //    _dbContextEntity = dbContextEntity;
        //}

        /// <summary>
        /// Generates Attestation PDF from submit data and uploads it to storage.
        /// </summary>
        /// <param name="submitDTO"></param>
        /// <returns></returns>
        public async Task<PdfDocumentResponse> HandlePDF(SubmitRequestDTO submitDTO,DateTime submitDate)
        {
            var providerId = submitDTO.Content.Setup.ProviderId;

            ProviderId = providerId;
            UploadFileName = _getAttestationFileName(providerId.ToString(),submitDate.ToString());
            UploadBy = submitDTO.Content.Setup.ProviderEmail;
            UploadDate = submitDate;

            var attestationRequestDTO = await GetAttestationDTO(providerId);//submitDTO

            return await HandlePDF(attestationRequestDTO);
        }

        static string GetAttestationFilename(string providerId, string submitDate)
        {
            return providerId + "_Attestation_" + submitDate+ ".pdf";
        }

        private async Task<AttestationPDFRequestDTO> GetAttestationDTO(int providerId)//submitDTO
        {
            _attestationRepository.SetDbContextTransaction(_dbContextEntity);
            var attestationEntity = _attestationRepository.GetAttestationByProviderIdAsync(providerId);

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
