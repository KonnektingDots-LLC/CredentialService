namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class MalpracticeCarrierListEntity:RecordHistoryTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region related entities

        public ICollection<MalpracticeEntity> Malpractice { get; set; }

        #endregion
    }
}
