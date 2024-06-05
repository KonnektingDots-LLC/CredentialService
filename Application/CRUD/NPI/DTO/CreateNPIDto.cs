using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Application.CRUD.NPI.DTO
{
    public class CreateNPIDto
    {
        public string? NPI { get; set; }
        [Required, MaxLength(12)]

        public int PNPITypeId { get; set; }

        public int ProviderId { get; set; }
    }
}
