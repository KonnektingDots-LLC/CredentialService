using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Application.Common.Helpers
{
    public static class EvidenceHelper
    {
        public static bool EvidenceHasBeenProvided(string evidenceFileName) 
        {
            return !evidenceFileName.IsNullOrEmpty();
        }
    }
}
