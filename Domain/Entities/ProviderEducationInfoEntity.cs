using cred_system_back_end_app.Domain.Common;

namespace cred_system_back_end_app.Domain.Entities
{
    public class ProviderEducationInfoEntity : ListMemberEntityBase
    {
        public int ProviderId { get; set; }
        public ProviderEntity Provider { get; set; }
        public int EducationInfoId { get; set; }
        public EducationInfoEntity EducationInfo { get; set; }
    }
}
