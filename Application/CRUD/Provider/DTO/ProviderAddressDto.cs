using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.CRUD.Provider.DTO
{
    public class ProviderAddressDto
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public AddressResponseDto Address { get; set; }
    }
}
