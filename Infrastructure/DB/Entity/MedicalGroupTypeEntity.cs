namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class MedicalGroupTypeEntity : RecordHistoryTypeList
    {
        public int Id { get; set; }

        public int Name { get; set; }

        #region related entities

        public ICollection<MedicalGroupEntity> MedicalGroup { get; set; }

        #endregion
    }
}
