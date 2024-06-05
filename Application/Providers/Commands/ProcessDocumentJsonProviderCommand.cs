using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Services.DTO;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Commands
{
    public record class ProcessDocumentJsonProviderCommand(List<MultiFileUploadDto> FileDetail, string Json, int ProviderId,
            string? FilesToDelete) : IRequest, ITransactionPipeline;
}
