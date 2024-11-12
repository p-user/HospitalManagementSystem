
using Authentication.Authentication.ServerConfiguration;
using Authentication.ServerConfiguration;
using IdentityModel.Client;
using System.Text.Json;

namespace Authentication.Authentication.Features.RefreshToken
{
    public record RefreshTokenCommand(string RefreshToken): IRequest<RefreshTokenCommandResponse>;
    public record RefreshTokenCommandResponse(TokenDto TokenDto);
    public class RefreshTokenCommandHandler(IHttpClientFactory _httpClientFactory, IConfiguration _configuration) : IRequestHandler<RefreshTokenCommand, RefreshTokenCommandResponse>
    {
        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            //get client app credential from config file
            var identityClient = Config.Clients.Where(s => s.ClientId == ConfigurationConstants.UserClient).FirstOrDefault();

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(_configuration.GetValue<string>("IdentityServer"));

            var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = identityClient.ClientId,
                ClientSecret = ConfigurationConstants.ClientSecret,
                RefreshToken = request.RefreshToken,

            });

            if (tokenResponse.IsError)
            {
                throw new Exception("Token was not refreshed!");
            }

            var response = JsonSerializer.Deserialize<TokenDto>(tokenResponse.Raw);
            return new RefreshTokenCommandResponse(response);
        }
    }
}
