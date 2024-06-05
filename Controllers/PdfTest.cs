using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Infrastructure.PdfReport.CredentialingApplication;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace cred_system_back_end_app.Controllers
{
    [Route("api")]
    [ApiController]
    public class PdfTest : ControllerBase
    {
        private readonly PdfGeneratorClient<IIPCAPdfRootDto> _pdfClient;


        public PdfTest(PdfGeneratorClient<IIPCAPdfRootDto> pdfClient)
        {
            _pdfClient = pdfClient;
        }

        [AuthorizeForScopes(Scopes = new[] { CredScope.READ_WRITE })]
        [HttpPost("[controller]/PdfGenerate")]
        public async Task<FileStreamResult> GeneratePdfAsync(IIPCAPdfRootDto root)
        {

            var result = await _pdfClient.GetPdfAsync(root, "IIPCAPdfHttpTrigger");
            var stream = await result.Content.ReadAsStreamAsync();

            return File(stream, "application/pdf");
        }
    }
}
