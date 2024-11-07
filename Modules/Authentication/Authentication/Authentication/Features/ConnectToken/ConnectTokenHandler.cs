

using IdentityServer;
using System.Net.Http;
using System.Text.Json;

namespace Authentication.Authentication.Features.ConnectToken
{
    public record ConnectTokenRequest(string Email, string Password) : IRequest<ConnectTokenResponse>;
    public record ConnectTokenResponse(string AccessToken, string TokenType,string RefreshToken, int ExpiresIn, string Scope);
    public class ConnectTokenHandler(IHttpClientFactory _httpClientFactory, IConfiguration _configuration) 
        : IRequestHandler<ConnectTokenRequest, ConnectTokenResponse>
    {
        public async  Task<ConnectTokenResponse> Handle(ConnectTokenRequest request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient();
            var identityClient = Config.Clients.Where(s => s.ClientId == "ResourceOwnerPasswordClient").FirstOrDefault();
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, _configuration["IdentityServer:TokenEndpoint"])
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", "password" }, //pfff
                    { "username", request.Email },
                    { "password", request.Password },
                    { "client_id",  identityClient.ClientId}, 
                    { "client_secret", identityClient.ClientSecrets.FirstOrDefault().Value }, 
                    { "scope", string.Join(" ", identityClient.AllowedScopes) } 
                })
            };

            var response = await client.SendAsync(tokenRequest);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<ConnectTokenResponse>(content);
            return tokenResponse;
        }
    }
}
