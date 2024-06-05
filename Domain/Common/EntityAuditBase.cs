using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Domain.Common
{
    public class EntityAuditBase
    {
        [AllowNull, MaxLength(255)]
        public string? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        [AllowNull, MaxLength(255)]
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
