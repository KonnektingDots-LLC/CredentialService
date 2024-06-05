using cred_system_back_end_app.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cred_system_back_end_app.Domain.Entities
{
    public class SubSpecialtyListEntity : EntityHistoryTypeList
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
