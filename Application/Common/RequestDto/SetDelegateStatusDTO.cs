namespace cred_system_back_end_app.Application.Common.RequestDto
{
    public class SetDelegateStatusDTO
    {
        public int DelegateId { get; set; }
        public bool IsActive { get; set; }
    }
}
