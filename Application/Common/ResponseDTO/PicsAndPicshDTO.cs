using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.Common.ResponseDTO
{
    public class PicsAndPicshDTO
    {
        public ProviderInsurerCompanyStatusEntity ProviderInsurerCompanyStatus { get; set; }
        public ProviderInsurerCompanyStatusHistoryEntity ProviderInsurerCompanyStatusHistory { get; set; }
    }
}
