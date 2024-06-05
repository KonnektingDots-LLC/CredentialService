using System.ComponentModel.DataAnnotations;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class BoardDocumentEntity : RecordHistory
    {       
        public int BoardId { get; set; }
        public string AzureBlobFilename { get; set; }

        #region relationships
        public BoardEntity Board { get; set; }
        public DocumentLocationEntity DocumentLocation { get; set; }
        #endregion
    }
}
