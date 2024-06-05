using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class MedicalGroupTypeEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }

        public int Name { get; set; }

        #region related entities

        public ICollection<MedicalGroupEntity> MedicalGroup { get; set; }

        #endregion
    }
}
