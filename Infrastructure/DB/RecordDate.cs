using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Infrastructure.DB
{
    public class RecordDate
    {
        [MaxLength(255)]
        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        [MaxLength(255)]
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
