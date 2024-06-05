namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class CorporationSubSpecialtyEntity:RecordHistory
    {
        public int CorporationEntityId { get; set; }
        public int SubSpecialtyListEntityId { get; set; }

        public CorporationEntity Corporation { get; set; }
    }
}
