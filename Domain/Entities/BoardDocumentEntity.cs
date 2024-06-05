using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class BoardDocumentEntity : EntityCommon
    {
        public int BoardId { get; set; }
        public string AzureBlobFilename { get; set; }

        #region relationships
        public BoardEntity Board { get; set; }
        public DocumentLocationEntity DocumentLocation { get; set; }
        #endregion
    }
}
