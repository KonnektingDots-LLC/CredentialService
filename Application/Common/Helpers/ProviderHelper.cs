using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Application.Common.Helpers
{
    public static class ProviderHelper
    {
        public static string GetFullName(this ProviderEntity provider) 
        {
            return $"{provider.FirstName} {StringHelper.PrintIfExists(provider.MiddleName)} {provider.LastName} {provider.SurName}";
        }
    }
}
