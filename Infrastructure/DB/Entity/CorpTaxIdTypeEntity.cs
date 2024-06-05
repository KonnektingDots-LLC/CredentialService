namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class CorpTaxIdTypeEntity:RecordHistoryTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CorporationEntity> Corporation { get; set; }
    }
}
