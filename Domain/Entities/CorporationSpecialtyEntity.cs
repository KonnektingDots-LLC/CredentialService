using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class CorporationSpecialtyEntity : EntityCommon
    {
        public int Id { get; set; }
        public int CorporationId { get; set; }
        public int SpecialtyId { get; set; }
    }
}
