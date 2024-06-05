using cred_system_back_end_app.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace cred_system_back_end_app.Domain.Entities
{
    public class ProviderSpecialtyEntity : EntityCommon
    {
        public int ProviderId { get; set; }
        public ProviderEntity Provider { get; set; }
        public int SpecialtyListId { get; set; }
        public SpecialtyListEntity SpecialtyList { get; set; }

        [Column("AzureBlobFilename")]
        public string AzureBlobFileName { get; set; }

        public DocumentLocationEntity DocumentLocation { get; set; }
    }
}
