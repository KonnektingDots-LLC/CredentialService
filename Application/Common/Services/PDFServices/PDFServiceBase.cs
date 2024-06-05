using cred_system_back_end_app.Application.CRUD.Document;
using cred_system_back_end_app.Application.CRUD.Document.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.PdfReport.CredentialingApplication;

namespace cred_system_back_end_app.Application.Common.Services.PDFServices
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
        private readonly DocumentCase _documentCase;
        private DbContextEntity _dbContextEntity;

        /// <inheritdoc/>
        public PDFServiceBase(PdfGeneratorClient<TPdfDTO> pdfGeneratorClient,
            DocumentCase documentCase,
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

        public void SetDbContextTransaction(DbContextEntity dbContextEntity)
        {
            _dbContextEntity = dbContextEntity;
        }
    }
}
