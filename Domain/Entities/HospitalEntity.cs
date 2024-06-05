using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class HospitalEntity : EntityCommon
    {
        public int Id { get; set; }

        public int HospPriviledgeListId { get; set; }
        public string? HospitalPrivilegesTypeOther { get; set; }

        public HospPriviledgeListEntity HospPriviledgeList { get; set; }

        public int HospitalPriviledgePeriodId { get; set; }

        public int HospitalListId { get; set; }
        public string? HospitalOther { get; set; }

        public bool IsSecondary { get; set; }

        #region related entities

        public PeriodEntity HospitalPriviledgePeriod { get; set; }

        public HospitalListEntity HospitalList { get; set; }

        public List<ProviderEntity> Provider { get; } = new();

        #endregion
    }
}
