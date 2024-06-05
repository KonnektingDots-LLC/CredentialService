using cred_system_back_end_app.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Domain.Entities
{
    public class InsurerTypeEntity : EntityCommon
    {
        public int Id { get; set; }
        [Required]
        public int InsurerTypeListId { get; set; }
        [Required]
        public int InsurerCompanyId { get; set; }
        [Required]
        public string? InsurerName { get; set; }
        [Required]
        public string? InsurerEmail { get; set; }
    }
}
