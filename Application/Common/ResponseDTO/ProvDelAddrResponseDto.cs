using cred_system_back_end_app.Application.Common.ResponseTO;

namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class ProvDelAddrResponseDto
    {
        public int DelegateId { get; set; }

        public string DelegateName { get; set; }
        //public AddressResponseDto Address { get; set; }
        public ProviderResponseDto Provider { get; set; }
    }
}
