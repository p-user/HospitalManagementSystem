

using System.Net.Http;
using System.Text.Json;

namespace Authentication.Authentication.Features.ConnectToken
{
    public record ConnectTokenRequest(string Email, string Password) : IRequest<ConnectTokenResponse>;
    public record ConnectTokenResponse(string AccessToken, string TokenType,string RefreshToken, int Expiresin, string Scope);
    public class ConnectTokenHandler(IHttpClientFactory _httpClientFactory, IConfiguration _configuration) : IRequestHandler<ConnectTokenRequest, ConnectTokenResponse>
    {
        public async  Task<ConnectTokenResponse> Handle(ConnectTokenRequest request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient();
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, _configuration["IdentityServer:TokenEndpoint"])
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "username", request.Email },
                { "password", request.Password },
                { "client_id", "swagger" },  // TODO : Specify  client_id from IdentityServer
                { "client_secret", "yourClientSecret" }, //TODO:  Specify  client secret
                { "scope", "openid api1" }  // Specify the scope
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
