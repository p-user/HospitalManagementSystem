using Duende.IdentityServer.Models;

namespace Authentication.Data.Configurations
{
    public static class Config
    {
        // API scopes
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
            new ApiScope("openid", "My OpenID Scope"),
            new ApiScope("api1", "My API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        // OAuth2 clients
        public static IEnumerable<Client> Clients =>
            new Client[]
            { 
                // machine to machine client (from quickstart 1)
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "api1" }
            },
            new Client
            {
                ClientId = "swagger",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,  
                AllowedScopes = { "openid", "api1" },
                AllowOfflineAccess= true,
            }
            };
    }

}
