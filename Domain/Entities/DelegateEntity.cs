using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class DelegateEntity : EntityCommon
    {
        public int Id { get; set; }
        public int DelegateTypeId { get; set; }
        public DelegateTypeEntity? DelegateType { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public int DelegateCompanyId { get; set; }
        public DelegateCompanyEntity? DelegateCompany { get; set; }

        public ICollection<ProviderDelegateEntity>? ProviderDelegate { get; set; }

    }
}
