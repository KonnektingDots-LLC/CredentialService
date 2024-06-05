using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Services.DTO;
using MediatR;
using Newtonsoft.Json;

namespace cred_system_back_end_app.Application.Providers.Commands.Handlers
{
    public class ProcessDocumentJsonProviderHandler : IRequestHandler<ProcessDocumentJsonProviderCommand>
    {
        private readonly IMultiFileUploadService _multiFileUploadService;

        public ProcessDocumentJsonProviderHandler(IMultiFileUploadService multiFileUploadService)
        {
            _multiFileUploadService = multiFileUploadService;
        }

        public async Task Handle(ProcessDocumentJsonProviderCommand request, CancellationToken cancellationToken)
        {
            await ProcessDocumentJsonProvider(request.FileDetail, request.Json, request.ProviderId, request.FilesToDelete);
        }

        public async Task ProcessDocumentJsonProvider(List<MultiFileUploadDto> fileDetail, string json, int providerId,
            string? filesToDelete)
        {
            if (providerId == 0) { throw new ProviderNotFoundException(); }

            if (!string.IsNullOrEmpty(filesToDelete))
            {
                List<FileToDeleteDto> filesToDeleteList = JsonConvert.DeserializeObject<List<FileToDeleteDto>>(filesToDelete);

                if (filesToDeleteList != null && filesToDeleteList.Count > 0)
                {
                    await _multiFileUploadService.DeleteFilesAsync(filesToDeleteList, providerId);
                }
            }

            if (fileDetail.Count > 0)
            {
                await _multiFileUploadService.UploadMultiDocumentAsync(fileDetail, providerId);
            }

            await _multiFileUploadService.AddJsonProviderForm(json, providerId);
        }
    }
}
