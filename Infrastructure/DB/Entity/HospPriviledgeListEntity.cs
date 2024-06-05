namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class HospPriviledgeListEntity : RecordHistoryTypeList
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        #region dependent entities

        public ICollection<HospitalEntity> Hospital { get; set; }

        #endregion
    }
}
