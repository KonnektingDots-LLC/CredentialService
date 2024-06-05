using cred_system_back_end_app.Application.DTO;
using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Application.Common.Mappers.EntityToDTO
{
    public static class ProviderMapper
    {
        public static ProviderBaseDto GetProviderResponseDTO(this ProviderEntity providerEntity)
        {
            return new ProviderBaseDto
            {
                ProviderId = providerEntity.Id,
                Name = providerEntity.FirstName,
                MiddleName = providerEntity.MiddleName,
                LastName = providerEntity.LastName,
                SurName = providerEntity.SurName,
                Email = providerEntity.Email,
                PhoneNumber = providerEntity.PhoneNumber,
                BillingNPI = providerEntity.BillingNPI,
                RenderingNPI = providerEntity.RenderingNPI,
                CredFormId = providerEntity.CredFormId,
                StatusName = providerEntity.CredForm.CredFormStatusType.Name
            };
        }
    }
}
