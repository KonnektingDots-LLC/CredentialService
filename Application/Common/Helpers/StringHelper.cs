using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Application.Common.Helpers
{
    public static class StringHelper
    {
        public static string PrintIfExists(string theString) 
        { 
            return theString.IsNullOrEmpty() ? "" : theString;
        }
    }
}
