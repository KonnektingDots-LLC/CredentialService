using cred_system_back_end_app.Application.DTO.Documents;

namespace cred_system_back_end_app.Domain.Interfaces.pdf
{
    public interface IAttestationPdfService
    {
        Task<PdfDocumentResponse> HandlePDF(SubmitRequestDTO submitDTO, DateTime submitDate);
    }
}
