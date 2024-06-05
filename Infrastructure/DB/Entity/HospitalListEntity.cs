namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class HospitalListEntity : RecordHistoryTypeList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BusinessName { get; set; }

        #region related entities

        public ICollection<HospitalEntity> Hospitals { get; set; }

        #endregion
    }
}
