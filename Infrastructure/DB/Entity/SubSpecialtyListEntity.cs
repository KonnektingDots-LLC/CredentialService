using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class SubSpecialtyListEntity : RecordHistoryTypeList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int OrganizationTypeId { get; set; }

        public OrganizationTypeEntity OrganizationType { get; set; }

        public string Name { get; set; }

        public List<CorporationEntity> Corporation { get; } = new();
    }
}
