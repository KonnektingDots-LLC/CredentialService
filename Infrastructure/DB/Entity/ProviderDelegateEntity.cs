using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ProviderDelegateEntity:RecordHistory
    {       
        public int Id { get; set; }

        public int ProviderId { get; set; }
        public ProviderEntity Provider { get; set; }

        public int DelegateId { get; set; }
        public DelegateEntity Delegate { get; set; }

    }
}
