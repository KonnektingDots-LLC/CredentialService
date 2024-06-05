using cred_system_back_end_app.Infrastructure.DB.Entity;
using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.FileSystem.GetDocument.DTO
{
    public class DocumentByProviderResponseDto
    {
        public string AzureBlobFilename { get; set; }
        [JsonProperty("LabelName")]
        public string Name { get; set; }
        public string SpecialtyName { get; set; }
        public string SubSpecialtyName { get; set; }

    }
}
