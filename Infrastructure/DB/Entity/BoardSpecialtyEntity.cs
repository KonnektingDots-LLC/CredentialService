using System.Reflection.Metadata.Ecma335;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class BoardSpecialtyEntity : RecordHistory
    {
        public int BoardId { get; set; }
        public int SpecialtyId { get; set; }

        #region related entities

        public BoardEntity Board { get; set; }

        #endregion
    }
}
