using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class DelegateCompanyEntity:RecordHistory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RepresentativeFullName { get; set; }
        public string RepresentativeEmail { get; set; }

        public ICollection<DelegateEntity> Delegate { get; set; }        

    }
}
