using Authentication.Authentication.ServerConfiguration;
using Authentication.ServerConfiguration;
using IdentityModel.Client;

namespace Authentication.Authentication.Features.LogOut
{
    public record LogOutCommand(string RefreshToken): IRequest<LogOutCommandResponse>;
    public record LogOutCommandResponse();
    public class LogOutCommandHandler(IHttpClientFactory _httpClientFactory, IConfiguration _configuration) : IRequestHandler<LogOutCommand, LogOutCommandResponse>
    {
        public async Task<LogOutCommandResponse> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            //get client app credential from config file
            var identityClient = Config.Clients.Where(s => s.ClientId == ConfigurationConstants.UserClient).FirstOrDefault();

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(_configuration.GetValue<string>("IdentityServer"));

            var tokenRevokeResponse = await client.RevokeTokenAsync(new TokenRevocationRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = identityClient.ClientId,
                ClientSecret = ConfigurationConstants.ClientSecret,
                Token= request.RefreshToken,
               
            });

            if (tokenRevokeResponse.IsError)
            {
                throw new Exception("LogOut process was not completed");
            }
                return new LogOutCommandResponse();

        }
    }
}
