using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class DelegateTypeEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<DelegateEntity>? Delegate { get; set; }

    }
}
