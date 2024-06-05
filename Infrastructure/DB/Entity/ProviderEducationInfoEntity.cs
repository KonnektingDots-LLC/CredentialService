namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class ProviderEducationInfoEntity: ListMemberEntityBase
    {
        public int ProviderId { get; set; }
        public ProviderEntity Provider { get; set; }
        public int EducationInfoId { get; set; }
        public EducationInfoEntity EducationInfo { get; set; }
    }
}
