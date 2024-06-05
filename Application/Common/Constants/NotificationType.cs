using System.ComponentModel;
using System.Security.AccessControl;

namespace cred_system_back_end_app.Application.Common.Constants
{
    public static class NotificationType
    {
        [Description("Delegate Invitation")]
        public const string DELEGATE_INV = "DELG_INV";   
        public const string PROVIDER_REV = "PROV_REV";
        public const string INSURER_INV = "INSR_INV";

        [Description("Delegate Status Update")]
        public const string DELEGATE_STATUS_UPDATE = "DELG_STAT";

        [Description("Profile Completion")]
        public const string PROFILE_COMPL = "PROF_COMPL";

        [Description("Provider Submit to Insurer")]
        public const string PROVIDER_INSURER_SUB = "PI_SUB";

        [Description("Provider Submit")]
        public const string PROVIDER_SUB = "PROV_SUB";

        [Description("Provider Submit to Delegate")]
        public const string PROVIDER_DELEGATE_SUB = "PD_SUB";

        [Description("Insurer to Provider Status")]
        public const string INSURER_PROVIDER_STATUS = "IP_STATUS";
    }
}
