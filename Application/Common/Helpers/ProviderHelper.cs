using cred_system_back_end_app.Domain.Entities;

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
