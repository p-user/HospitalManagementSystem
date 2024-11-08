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
                ClientId = "HospitalClient",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, 
                AllowOfflineAccess = true,
                // scopes that client has access to
                AllowedScopes = { "hospitalApi" } //give the key of the scope
            },

          };
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("hospitalApi", "Hospital API")
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
