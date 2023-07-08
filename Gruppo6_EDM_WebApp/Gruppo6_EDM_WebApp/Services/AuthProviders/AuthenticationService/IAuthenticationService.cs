using Entities.DTO;

namespace Gruppo6_EDM_WebApp.Services.AuthProviders.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication);
        Task Logout();
    }
}