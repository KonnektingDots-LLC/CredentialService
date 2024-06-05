namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ResidencyEntity : RecordHistory
    {
        public int Id { get; set; }

        public int EducationInfoId { get; set; }

        public DateTime PostGraduateCompletionDate { get; set; }

        #region related entities

        public EducationInfoEntity EducationInfo { get; set; }

        #endregion
    }
}
