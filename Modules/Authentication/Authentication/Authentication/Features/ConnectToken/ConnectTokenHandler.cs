
using Authentication.Authentication.ServerConfiguration;
using Authentication.ServerConfiguration;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using IdentityModel.Client;

namespace Authentication.Authentication.Features.ConnectToken
{
    public record ConnectTokenRequest(string Email, string Password) : IRequest<ConnectTokenResponse>;
    public record ConnectTokenResponse(string AccessToken, string TokenType, string RefreshToken, int ExpiresIn);
    public class ConnectTokenHandler(IHttpClientFactory _httpClientFactory, IConfiguration _configuration)
        : IRequestHandler<ConnectTokenRequest, ConnectTokenResponse>
    {
        public async Task<ConnectTokenResponse> Handle(ConnectTokenRequest request, CancellationToken cancellationToken)
        {
            //get client app credential from config file
            var identityClient = Config.Clients.Where(s => s.ClientId == ConfigurationConstants.UserClient).FirstOrDefault();



            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(_configuration.GetValue<string>("IdentityServer"));

            var token = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = identityClient.ClientId,
                ClientSecret = ConfigurationConstants.ClientSecret,
                Scope = string.Join(" ", identityClient.AllowedScopes),
                UserName = request.Email,
                Password = request.Password,
            });





            return new ConnectTokenResponse(token.AccessToken, "Bearer", token.RefreshToken, token.ExpiresIn);


        }
    }
}