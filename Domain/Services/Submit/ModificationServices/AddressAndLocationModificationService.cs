using AutoMapper;
using cred_system_back_end_app.Domain.Services.Submit.DTO;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Domain.Services.Submit.ModificationServices
{
    public class AddressAndLocationModificationService : EntityModificationServiceBase
    {
        private readonly DbContextEntity _dbContextEntity;

        public AddressAndLocationModificationService
        (
            DbContextEntity dbContextEntity,
            IMapper mapper
        ) : base(dbContextEntity, mapper)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task Modify(IEnumerable<AddressAndLocationDTO> addressAndLocationDTOs, int providerId)
        {
            var newProviderAddresses = Application.Common.Mappers.DTOToEntity.Provider
                .GetAllProviderAddressEntities(addressAndLocationDTOs, providerId);

            var oldProviderAddresses = _dbContextEntity.ProviderAddress
                .Where(p => p.ProviderId == providerId)
                .Include(p => p.Address)
                .Include(p => p.Address.AddressServiceHours)
                .ToList();

            await ModifyList
            (
                oldProviderAddresses,
                newProviderAddresses,
                newProviderAddress => UpdateListMember(newProviderAddress, oldProviderAddresses),
                newProviderAddress => AddListMember(newProviderAddress),
                addressesToDelete => RemoveListMembers(addressesToDelete)
            );
        }
    }
}
