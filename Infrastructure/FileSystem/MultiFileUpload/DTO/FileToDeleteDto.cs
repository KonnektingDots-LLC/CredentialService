using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.FileSystem.MultiFileUpload.DTO
{
    public class FileToDeleteDto
    {
        [JsonProperty("uploadFilename")]       
        public string UploadFilename { get; set; }
        [JsonProperty("documentTypeId")]
        public int DocumentTypeId { get; set; }
    }
}
