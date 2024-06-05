namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class EducationTypesEntity : RecordHistory
    {
        public int Id { get; set; }

        public string EducationTypeName { get; set; }

        #region related entities

        public ICollection<EducationInfoEntity> EducationInfo { get; set; }

        #endregion
    }
}
