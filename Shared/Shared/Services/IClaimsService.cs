

namespace Shared.Services
{
    public interface IClaimsService
    {

        IDictionary<string, string> GetUserClaims();
        string GetCurrentUser();
        string GetCurrentUserEmail();
    }
}
