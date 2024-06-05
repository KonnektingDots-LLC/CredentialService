using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class EducationTypesEntity : EntityCommon
    {
        public int Id { get; set; }

        public string EducationTypeName { get; set; }

        #region related entities

        public ICollection<EducationInfoEntity> EducationInfo { get; set; }

        #endregion
    }
}
