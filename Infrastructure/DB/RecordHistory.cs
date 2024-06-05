using cred_system_back_end_app.Controllers;
using cred_system_back_end_app.Infrastructure.B2C;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Infrastructure.DB
{
    
    public class RecordHistory
    {

        [MaxLength(255)]
        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; } = DateTime.Now;
        [MaxLength(255)]
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
