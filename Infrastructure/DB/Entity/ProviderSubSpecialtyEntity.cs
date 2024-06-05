using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ProviderSubSpecialtyEntity : RecordHistory
    {
        public int ProviderId { get; set; }
        public ProviderEntity Provider { get; set; }
        public int SubSpecialtyListId { get; set; }
        public SubSpecialtyListEntity SubSpecialtyList { get; set; }
        [Column("AzureBlobFilename")]
        public string DocumentLocationId { get; set; }
        public DocumentLocationEntity DocumentLocation { get; set; }
    }
}
