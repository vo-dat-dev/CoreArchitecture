using AuthenticationApi.Dtos;

namespace AuthenticationApi.Interface
{
    public interface IAuthentication
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<string> AuthenticationTest();
    }
};