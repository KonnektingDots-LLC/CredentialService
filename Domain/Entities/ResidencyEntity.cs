using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class ResidencyEntity : EntityCommon
    {
        public int Id { get; set; }

        public int EducationInfoId { get; set; }

        public DateTime PostGraduateCompletionDate { get; set; }

        #region related entities

        public EducationInfoEntity EducationInfo { get; set; }

        #endregion
    }
}
