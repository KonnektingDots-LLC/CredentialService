using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class HospPriviledgeListEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        #region dependent entities

        public ICollection<HospitalEntity> Hospital { get; set; }

        #endregion
    }
}
