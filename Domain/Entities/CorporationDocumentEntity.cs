using cred_system_back_end_app.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cred_system_back_end_app.Domain.Entities
{
    public class CorporationDocumentEntity : EntityCommon
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
