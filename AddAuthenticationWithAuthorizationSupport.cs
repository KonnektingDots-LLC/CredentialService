using Microsoft.AspNetCore.Authorization;

namespace cred_system_back_end_app
{

    public static class AuthenticationServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthenticationWithAuthorizationSupport(this IServiceCollection services, IConfiguration config)
        {
            //services.AddMicrosoftIdentityWebApiAuthentication(config, "AzureAdB2C");

            services.AddSingleton<IAuthorizationHandler, ScopesHandler>();

            return services;
        }
    }
}

