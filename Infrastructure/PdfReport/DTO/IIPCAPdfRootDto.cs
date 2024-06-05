using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class IIPCAPdfRootDto
    {
        [JsonProperty("FormSections")]
        public FormSectionsDto FormSections { get; set; } = new FormSectionsDto();
    }
}
