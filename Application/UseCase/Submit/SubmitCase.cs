using Microsoft.AspNetCore.Mvc;
using cred_system_back_end_app.Application.Common.Services;
using cred_system_back_end_app.Application.Common.Services.PDFServices;
using cred_system_back_end_app.Application.Common.Services.PDFServices.AttestationPDF;
using cred_system_back_end_app.Application.CRUD.Document.DTO;
using cred_system_back_end_app.Application.CRUD.Provider;
using cred_system_back_end_app.Application.UseCase.Notifications.NotificationManagers;
using cred_system_back_end_app.Application.CRUD.MedicalGroup.DTO;

namespace cred_system_back_end_app.Application.UseCase.Submit
{
    /// <summary>
    /// Handles Submit data flow.
    /// </summary>
    public class SubmitCase
    {
        private readonly IIPCAPDFService _iIPCAPdfService;
        private readonly AttestationPDFService _attestationPDFService;
        private readonly SubmitDBContextManager _submitDBContextManager;
        private readonly ILogger<SubmitCase> _logger;
        private readonly FireAndForgetService _fireAndForgetService;
        private readonly ProviderRepository _providerRepository;
        private DateTime submitDate;

        /// <summary>
        /// Creates an instance of SubmitCase
        /// </summary>
        public SubmitCase(
            ProviderRepository providerRepository,
            SubmitDBContextManager dbContextManager, 
            IIPCAPDFService iIPCAPdfService,
            AttestationPDFService attestationPDFService,
            FireAndForgetService fireAndForgetService,
            ILogger<SubmitCase> logger)
        {
            _iIPCAPdfService = iIPCAPdfService;
            _attestationPDFService = attestationPDFService;
            _submitDBContextManager = dbContextManager;
            _logger = logger;
            _fireAndForgetService = fireAndForgetService;
            _providerRepository = providerRepository;
        }

        /// <summary>
        /// Saves submit data,
        /// sends notifications to associated insurers and delegates,
        /// and generates PDF documents.
        /// </summary>
        /// <param name="submitData"></param>
        /// <returns></returns>
        public async Task<PdfDocumentResponse> SubmitAll([FromBody]SubmitRequestDTO submitData, string email)
        {
            try
            {
                var dbContextBeginTransaction = _submitDBContextManager.DBBeginTransaction();
                submitDate = DateTime.Now;
                _submitDBContextManager.SetSubmitDate(submitDate);

                var isFirstSubmit = await IsFirstSubmit(submitData);

                await SaveSubmitData(submitData, isFirstSubmit, email);
                                        
                var pdfs = await GeneratePDFs(submitData, submitDate);

                var providerInsurerCompanyStatusIds = _submitDBContextManager.GetProviderInsurerCompanyStatusIds();

                _submitDBContextManager.Commit(dbContextBeginTransaction);
                
                SendSubmitNotifications(submitData, isFirstSubmit, providerInsurerCompanyStatusIds);

                return pdfs;
            }
            catch (Exception ex) {     

                _logger.LogInformation(ex.ToString());
                
                throw ex; 
            }
        }

        #region helpers

        private async Task<bool> IsFirstSubmit(SubmitRequestDTO submitRequestDTO)
        {
            var providerDetail = await _providerRepository.GetProviderDetailByProviderId(submitRequestDTO.Content.Setup.ProviderId);

            return providerDetail == null ? true : false;
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

            _submitDBContextManager.Save();

            //await _submitDBContextManager.SaveAndCommit();
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

            //await _submitDBContextManager.Save();
        }

        private async Task CreateCredentialingStatus(SubmitRequestDTO submitData, string modifiedBy)
        {
            await _submitDBContextManager.CreateCredentialingStatus(submitData, modifiedBy,submitDate);

            //await _submitDBContextManager.Save();
        }

        private async Task<PdfDocumentResponse> GeneratePDFs(SubmitRequestDTO submitData, DateTime submitDate)
        {
            var submitDBContextTransaction = _submitDBContextManager.GetDbContextTransaction();
            _attestationPDFService.SetDbContextTransaction(submitDBContextTransaction);
            await _attestationPDFService.HandlePDF(submitData,submitDate);

            _iIPCAPdfService.SetDbContextTransaction(submitDBContextTransaction);
            return await _iIPCAPdfService.HandlePDF(submitData,submitDate);
        }

        public DateTime getSubmitDate()
        {
            return submitDate;
        }

        #endregion
    }
}
