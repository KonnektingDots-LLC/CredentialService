using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class BoardSpecialtyEntity : EntityCommon
    {
        public int BoardId { get; set; }
        public int SpecialtyId { get; set; }

        #region related entities

        public BoardEntity Board { get; set; }

        #endregion
    }
}
