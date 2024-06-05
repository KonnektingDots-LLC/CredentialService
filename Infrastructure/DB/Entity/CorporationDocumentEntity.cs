using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class CorporationDocumentEntity:RecordHistory
    {
        [Key]
        public int CorporationId { get; set; }
        public CorporationEntity Corporation { get; set; }
        [Key]
        [Column("AzureBlobFilename")]
        public string DocumentLocationId { get; set; }
        public DocumentLocationEntity DocumentLocation { get; set; }

    }
}
