using cred_system_back_end_app.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Domain.Entities
{
    public class ProviderTypeEntity : EntityCommon
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public bool IsExpired { get; set; }
        public DateTime? ExpiredDate { get; set; }
        /* BEGIN EF Relation */
        public List<ProviderEntity> Provider { get; set; }
    }
}
