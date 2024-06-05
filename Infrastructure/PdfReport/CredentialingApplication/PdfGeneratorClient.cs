using Newtonsoft.Json;
using System.Text;

namespace cred_system_back_end_app.Infrastructure.PdfReport.CredentialingApplication
{
    public class PdfGeneratorClient<TPdfDTO>
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PdfGeneratorClient(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }
        public async Task<HttpResponseMessage> GetPdfAsync(TPdfDTO pdfDTO, string suffix)
        {
            try
            {
                var json = JsonConvert.SerializeObject(pdfDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    // keyvault here 
                    RequestUri = new Uri($"{_configuration["AzureFunctionPdf"]}/{suffix}")
                };
                request.Content = content;

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else
                {
                    throw new Exception(suffix + ": " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
