using Duende.IdentityServer.Models;

namespace IdentityServer
{
    public static class Config
    {
        // API scopes
        public static IEnumerable<Client> Clients =>
          new Client[]
          { 
              
            new Client
            {
                ClientId = "HospitalClient",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "hospitalApi" } //give the key of the scope
            },
             new Client
            {
                ClientId = "ResourceOwnerPasswordClient",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "hospitalApi" } //give the key of the scope
            },

          };
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {           
                new ApiScope("hospitalApi", "Hospital API")
            };

      
    }

}
