using CoreArchitecture.Dtos;

namespace CoreArchitecture.Interface
{
    public interface IAuthentication
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<string> AuthenticationTest();
    }
};