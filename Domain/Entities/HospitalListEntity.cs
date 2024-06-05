using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class HospitalListEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string BusinessName { get; set; }

        #region related entities

        public ICollection<HospitalEntity> Hospitals { get; set; }

        #endregion
    }
}
