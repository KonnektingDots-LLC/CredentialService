using cred_system_back_end_app.Application.DTO.Documents;
using cred_system_back_end_app.Infrastructure.AzureBlobStorage;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using cred_system_back_end_app.Infrastructure.PdfReport.CredentialingApplication;

namespace cred_system_back_end_app.Infrastructure.PdfReport.PDFServices
{
    /// <summary>
    /// Hanldes general procedure of generating PDFs and uploading them to storage.
    /// </summary>
    public class PDFServiceBase<TPdfDTO>
    {
        protected int ProviderId;
        protected string UploadBy;
        protected string UploadFileName;
        protected int DocumentType;
        protected string PDFGeneratorApiSuffix;
        protected DateTime UploadDate;


        private readonly PdfGeneratorClient<TPdfDTO> _pdfGeneratorClient;
        private readonly DocumentUploadService _documentCase;
        private readonly DbContextEntity _dbContextEntity;

        /// <inheritdoc/>
        public PDFServiceBase(PdfGeneratorClient<TPdfDTO> pdfGeneratorClient,
            DocumentUploadService documentCase,
            DbContextEntity dbContextEntity)
        {
            _pdfGeneratorClient = pdfGeneratorClient;
            _documentCase = documentCase;
            _dbContextEntity = dbContextEntity;
        }

        /// <summary>
        /// Generates PDF and uploads it to storage.
        /// </summary>
        /// <param name="pdfRequestDTO"></param>
        /// <returns></returns>
        public async Task<PdfDocumentResponse> HandlePDF(TPdfDTO pdfRequestDTO)
        {
            var response = await _pdfGeneratorClient.GetPdfAsync(pdfRequestDTO, PDFGeneratorApiSuffix);

            var pdfStream = await response.Content.ReadAsStreamAsync();

            var pdfUploadDTO = new PdfUploadDto
            {
                ProviderId = ProviderId,
                UploadFileName = UploadFileName,
                PdfStream = pdfStream,
                UploadBy = UploadBy,
                DocumentTypeId = DocumentType,
                UploadDate = UploadDate
            };

            _documentCase.SetDbContextTransaction(_dbContextEntity);
            return await _documentCase.UploadPDFAsync(pdfUploadDTO);
        }
    }
}
