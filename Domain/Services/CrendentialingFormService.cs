using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Repositories;

namespace cred_system_back_end_app.Domain.Services
{
    public class CrendentialingFormService : ICrendentialingFormService
    {
        private readonly IGenericRepository<CredFormStatusTypeEntity, string> _credFormStatusTypeRepository;
        private readonly IGenericRepository<StateTypeEntity, string> _stateTypeRepository;

        private readonly IJsonProviderFormRepository _jsonFormRepository;
        private readonly ICredFormRepository _credFormRepository;


        public CrendentialingFormService(IJsonProviderFormRepository jsonProviderFormRepository, ICredFormRepository credFormRepository,
            IGenericRepository<CredFormStatusTypeEntity, string> credFormStatusTypeRepository, IGenericRepository<StateTypeEntity, string> stateTypeRepository)
        {
            _jsonFormRepository = jsonProviderFormRepository;
            _credFormRepository = credFormRepository;
            _stateTypeRepository = stateTypeRepository;
            _credFormStatusTypeRepository = credFormStatusTypeRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="DeniedNewRecordException"></exception>
        /// <exception cref="EntityNotFoundException"></exception>
        public async Task<CredFormEntity?> CreateCredFormVersion(string email)
        {
            if (await _credFormRepository.GetByEmailAsync(email) != null)
            {
                throw new DeniedNewRecordException("A credentialing form already exists for provider.");
            }

            var newCredForm = new CredFormEntity
            {
                Email = email,
                Version = 1,
                CredFormStatusTypeId = StatusType.DRAFT,
                CreatedBy = email,
            };

            var credFormStatus = await _credFormStatusTypeRepository.GetByIdAsync(newCredForm.CredFormStatusTypeId)
                ?? throw new EntityNotFoundException($"Credentialing form status type was not found by id [{newCredForm.CredFormStatusTypeId}]");

            _ = await _stateTypeRepository.GetByIdAsync(credFormStatus.StateTypeId)
                ?? throw new EntityNotFoundException($"State type was not found by id [{credFormStatus.StateTypeId}]");

            return await _credFormRepository.AddAndSaveAsync(newCredForm);
        }

        /// <summary>
        /// Get a credentialing form by id. Only the general information is returned.
        /// </summary>
        /// <param name="credFormId"></param>
        /// <returns></returns>
        /// <exception cref="GenericCredentialingFormException"></exception>
        public async Task<CredFormEntity?> GetById(int credFormId)
        {
            var credFormEntity = await _credFormRepository.GetByIdAsync(credFormId)
                ?? throw new GenericCredentialingFormException($"Credentialing form was not found by id {credFormId}");
            return credFormEntity;
        }

        /// <summary>
        /// Get the latest json body (snapshot) for a provider
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// <exception cref="ProviderNotFoundException"></exception>
        public async Task<JsonProviderFormEntity> GetLatestSnapshotByProviderId(int providerId)
        {
            var existingJsonRecord = await _jsonFormRepository.GetLatestByProviderId(providerId);

            if (existingJsonRecord == null)
            {
                throw new ProviderNotFoundException($"Retreiving credentialing form JSON Body failed because provider {providerId} was not found.");
            }

            return existingJsonRecord;
        }

    }
}
