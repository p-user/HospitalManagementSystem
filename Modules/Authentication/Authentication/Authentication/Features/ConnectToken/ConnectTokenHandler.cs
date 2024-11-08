
using Authentication.ServerConfiguration;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using static MassTransit.ValidationResultExtensions;

namespace Authentication.Authentication.Features.ConnectToken
{
    public record ConnectTokenRequest(string Email, string Password) : IRequest<ConnectTokenResponse>;
    public record ConnectTokenResponse(string AccessToken, string TokenType,string RefreshToken, int ExpiresIn);
    public class ConnectTokenHandler(IHttpClientFactory _httpClientFactory, IConfiguration _configuration, IResourceOwnerPasswordValidator _passwordValidator, ITokenService _tokenService) 
        : IRequestHandler<ConnectTokenRequest, ConnectTokenResponse>
    {
        public async  Task<ConnectTokenResponse> Handle(ConnectTokenRequest request, CancellationToken cancellationToken)
        {
            //get client app credential from config file
            var identityClient = Config.Clients.Where(s => s.ClientId == "HospitalClient").FirstOrDefault();


            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7157");

            var token = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "HospitalClient",
                ClientSecret = "secret",
                Scope = "hospitalApi offline_access",
                UserName = request.Email,
                Password = request.Password,
            });

  



            return new ConnectTokenResponse(token.AccessToken, "Bearer", token.RefreshToken, token.ExpiresIn);
            
                
        }
    }
}
