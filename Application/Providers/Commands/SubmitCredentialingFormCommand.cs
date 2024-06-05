using cred_system_back_end_app.Application.DTO.Documents;
using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Commands
{
    public record class SubmitCredentialingFormCommand(SubmitRequestDTO SubmitData, string UserEmail) : IRequest<PdfDocumentResponse>, ITransactionPipeline;
}
