using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class CitizenshipTypeEntity:RecordHistoryTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CitizenshipTypeEntity> CitizenshipType { get; set; }

    }
}
