using SecureDevelopment.Models;
using SecureDevelopment.Models.Requests;

namespace SecureDevelopment.Services
{
    public interface IAuthenticateService
    {
        AuthenticationResponse Login(AuthenticationRequest authenticationRequest);

        public SessionInfo GetSessionInfo(string sessionToken);
    }
}
