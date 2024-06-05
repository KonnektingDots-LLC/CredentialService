namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class CorporationSpecialtyEntity:RecordHistory
    {
        public int Id { get; set; }
        public int CorporationId { get; set; }
        public int SpecialtyId { get; set; }
    }
}
