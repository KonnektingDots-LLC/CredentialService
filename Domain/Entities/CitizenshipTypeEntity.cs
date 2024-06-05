using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class CitizenshipTypeEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CitizenshipTypeEntity> CitizenshipType { get; set; }

    }
}
