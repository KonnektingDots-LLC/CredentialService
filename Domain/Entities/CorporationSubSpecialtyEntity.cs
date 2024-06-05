using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class CorporationSubSpecialtyEntity : EntityCommon
    {
        public int CorporationEntityId { get; set; }
        public int SubSpecialtyListEntityId { get; set; }

        public CorporationEntity Corporation { get; set; }
    }
}
