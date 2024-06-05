using cred_system_back_end_app.Application.DTO;
using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Application.Common.Mappers.EntityToDTO
{
    public static class PICSMapper
    {
        public static ProviderBaseDto GetPICSResponseDTO(this ProviderInsurerCompanyStatusEntity picsEntity)
        {
            return new ProviderBaseDto
            {
                ProviderId = picsEntity.Provider.Id,
                Name = picsEntity.Provider.FirstName,
                MiddleName = picsEntity.Provider.MiddleName,
                LastName = picsEntity.Provider.LastName,
                SurName = picsEntity.Provider.SurName,
                Email = picsEntity.Provider.Email,
                PhoneNumber = picsEntity.Provider.PhoneNumber,
                BillingNPI = picsEntity.Provider.BillingNPI,
                RenderingNPI = picsEntity.Provider.RenderingNPI,
                CredFormId = picsEntity.Provider.CredFormId,
                StatusName = picsEntity.InsurerStatusType.Name
            };
        }
    }
}
