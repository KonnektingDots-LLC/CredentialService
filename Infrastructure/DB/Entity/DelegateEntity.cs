using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class DelegateEntity:RecordHistory
    {
        public int Id { get; set; }

        public int DelegateTypeId { get; set; }
        public DelegateTypeEntity DelegateType { get; set; }

        public string FullName { get; set; }

        public string? Email { get; set; }

        public int DelegateCompanyId { get; set; }
        public DelegateCompanyEntity DelegateCompany { get; set; }

        public ICollection<ProviderDelegateEntity> ProviderDelegate { get; set; }

    }
}
