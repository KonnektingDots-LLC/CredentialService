using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class MalpracticeCarrierListEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region related entities

        public ICollection<MalpracticeEntity> Malpractice { get; set; }

        #endregion
    }
}
