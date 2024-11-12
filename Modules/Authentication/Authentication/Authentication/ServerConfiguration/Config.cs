using Authentication.Authentication.ServerConfiguration;
using Duende.IdentityServer.Models;

namespace Authentication.ServerConfiguration
{
    public static class Config
    {
        // API scopes
        public static IEnumerable<Client> Clients =>
          new Client[]
          {

             new Client
            {
                ClientId = ConfigurationConstants.UserClient,
                ClientSecrets = { new Secret(ConfigurationConstants.ClientSecret.Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowOfflineAccess = true,
                AccessTokenLifetime = 3600,
                AbsoluteRefreshTokenLifetime = 2592000,
                SlidingRefreshTokenLifetime = 1296000,
                // scopes that client has access to
                AllowedScopes = { "hospitalApi", "offline_access" } //give the key of the scope
            },
               new Client
            {
                ClientId = ConfigurationConstants.MachineToMachineClient,
                ClientSecrets = { new Secret(ConfigurationConstants.ClientSecret.Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowOfflineAccess = true,
                // scopes that client has access to
                AllowedScopes = { "hospitalApi", "offline_access" } //give the key of the scope
            },

          };
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("hospitalApi", "Hospital API"),
                new ApiScope("offline_access", "Offline access")
            };

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
        }


    }

}