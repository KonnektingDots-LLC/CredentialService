using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class DelegateCompanyEntity : EntityCommon
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? RepresentativeFullName { get; set; }
        public string? RepresentativeEmail { get; set; }

        public ICollection<DelegateEntity>? Delegate { get; set; }

    }
}
