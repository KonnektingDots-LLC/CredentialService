namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class ProviderByDelegateDto
    {
        public int DelegateId { get; set; }
        public string DelegateFullName { get; set; }
        public int ProviderId { get; set; }
        public string ProviderFullName { get; set; }
        public AddressResponseDto Address { get; set; }
    }
}
