using AuthenticationApi.Dtos;
using AuthenticationApi.Interface;

namespace AuthenticationApi.Implement
{
    public class AuthenticationImpl : IAuthentication
    {
        public Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task<string> AuthenticationTest()
        {
            return Task.FromResult("Testing");
        }
    }
};