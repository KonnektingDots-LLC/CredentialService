using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class ProfessionalLiabilityCarrierListEntity : EntityHistoryTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region related entities

        public ICollection<ProfessionalLiabilityEntity> ProfessionalLiability { get; set; }

        #endregion
    }
}
