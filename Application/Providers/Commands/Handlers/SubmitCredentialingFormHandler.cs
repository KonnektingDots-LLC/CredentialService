using cred_system_back_end_app.Application.Common.Services;
using cred_system_back_end_app.Application.DTO.Documents;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces.pdf;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Domain.Services.Submit;
using MediatR;

namespace cred_system_back_end_app.Application.Providers.Commands.Handlers
{
    /// <summary>
    /// Handles Submit data flow.
    /// </summary>
    public class SubmitCredentialingFormHandler : IRequestHandler<SubmitCredentialingFormCommand, PdfDocumentResponse>
    {
        private readonly IIipcaPdfService _iIPCAPdfService;
        private readonly IAttestationPdfService _attestationPDFService;
        private readonly SubmitDBContextManager _submitDBContextManager;
        private readonly FireAndForgetService _fireAndForgetService;
        private readonly IProviderDetailRepository _providerDetailRepository;
        private DateTime submitDate;

        /// <summary>
        /// Creates an instance of SubmitCase
        /// </summary>
        public SubmitCredentialingFormHandler(
            IProviderDetailRepository providerDetailRepository,
            SubmitDBContextManager dbContextManager,
            IIipcaPdfService iIPCAPdfService,
            IAttestationPdfService attestationPDFService,
            FireAndForgetService fireAndForgetService)
        {
            _iIPCAPdfService = iIPCAPdfService;
            _attestationPDFService = attestationPDFService;
            _submitDBContextManager = dbContextManager;
            _fireAndForgetService = fireAndForgetService;
            _providerDetailRepository = providerDetailRepository;
        }

        public async Task<PdfDocumentResponse> Handle(SubmitCredentialingFormCommand request, CancellationToken cancellationToken)
        {
            return await SubmitAll(request.SubmitData, request.UserEmail);
        }

        /// <summary>
        /// Saves submit data,
        /// sends notifications to associated insurers and delegates,
        /// and generates PDF documents.
        /// </summary>
        /// <param name="submitData"></param>
        /// <returns></returns>
        public async Task<PdfDocumentResponse> SubmitAll(SubmitRequestDTO submitData, string email)
        {
            try
            {
                submitDate = DateTime.Now;
                _submitDBContextManager.SetSubmitDate(submitDate);

                var isFirstSubmit = await IsFirstSubmit(submitData);

                await SaveSubmitData(submitData, isFirstSubmit, email);

                var pdfs = await GeneratePDFs(submitData, submitDate);

                var providerInsurerCompanyStatusIds = _submitDBContextManager.GetProviderInsurerCompanyStatusIds();

                SendSubmitNotifications(submitData, isFirstSubmit, providerInsurerCompanyStatusIds);

                return pdfs;
            }
            catch (Exception ex)
            {
                throw new GenericProviderException($"An exception ocurred when saving the credentialling form. {ex.Message}", ex);
            }
        }

        #region helpers

        private async Task<bool> IsFirstSubmit(SubmitRequestDTO submitRequestDTO)
        {
            var providerDetail = await _providerDetailRepository.GetByProviderId(submitRequestDTO.Content.Setup.ProviderId);

            return providerDetail == null;
        }

        private async Task SaveSubmitData(SubmitRequestDTO submitData, bool isFirstSubmit, string submittedBy)
        {
            if (isFirstSubmit)
            {
                await _submitDBContextManager.AddAllEntities(submitData, submittedBy);
                await CreateCredentialingStatus(submitData, submittedBy);
            }
            else
            {
                await _submitDBContextManager.ModifyEntities(submitData, submittedBy);
                await UpdateCredentialingStatus(submitData, submittedBy);
            }

            await _submitDBContextManager.SaveJsonProviderForm(submitData.JsonProviderForm, submitData.Content.Setup.ProviderId, submittedBy);

            await _submitDBContextManager.Save();
        }

        private void SendSubmitNotifications(SubmitRequestDTO submitData, bool isFirstSubmit, List<int>? providerInsurerCompanyStatusIds)
        {
            _fireAndForgetService
                .Execute<SubmitNotificationManager>
                (
                    async n => await n.SendNotificationsAsync
                    (
                        submitData.Content.Setup.ProviderId,
                        submitData.Content.Setup.ProviderEmail,
                        isFirstSubmit,
                        providerInsurerCompanyStatusIds
                    )
                );
        }

        private async Task UpdateCredentialingStatus(SubmitRequestDTO submitData, string modifiedBy)
        {
            await _submitDBContextManager.UpdateCredentialingStatus(submitData, modifiedBy, submitDate);
        }

        private async Task CreateCredentialingStatus(SubmitRequestDTO submitData, string modifiedBy)
        {
            await _submitDBContextManager.CreateCredentialingStatus(submitData, modifiedBy, submitDate);
        }

        private async Task<PdfDocumentResponse> GeneratePDFs(SubmitRequestDTO submitData, DateTime submitDate)
        {
            await _attestationPDFService.HandlePDF(submitData, submitDate);
            return await _iIPCAPdfService.HandlePDF(submitData, submitDate);
        }

        public DateTime getSubmitDate()
        {
            return submitDate;
        }

        #endregion
    }
}
