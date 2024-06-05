namespace cred_system_back_end_app.Infrastructure.Smtp.ProviderSubmitToInsurerNotification
{
    public class ProviderSubmitToInsurerRequestDto
    {
        //public string InsurerCompanyName { get; set; }
        public string ProviderName { get; set; }
        public string EmailTo { get; set; }
        public string Link {  get; set; }
        public int ProviderId { get; set; }
    }
}
