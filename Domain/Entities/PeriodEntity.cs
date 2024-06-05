using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class PeriodEntity : EntityCommon
    {
        public int Id { get; set; }

        public int PeriodMonthFrom { get; set; }

        public int PeriodYearFrom { get; set; }

        public int PeriodMonthTo { get; set; }

        public int PeriodYearTo { get; set; }

        #region related entities

        public HospitalEntity Hospital { get; set; }

        public EducationInfoEntity Education { get; set; }

        #endregion
    }
}
