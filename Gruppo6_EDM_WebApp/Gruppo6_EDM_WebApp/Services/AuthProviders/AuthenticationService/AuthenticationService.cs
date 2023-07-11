using Blazored.LocalStorage;
using Entities.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Gruppo6_EDM_WebApp.Services.AuthProviders.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }
        public async Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication)
        {
            var adminTokenEndpoint = "https://gruppo6-webapp.azurewebsites.net/api/Token/admin";
            var userTokenEndpoint = "https://gruppo6-webapp.azurewebsites.net/api/Token/user";

            var adminAuthResult = await TryAuthenticate(adminTokenEndpoint, userForAuthentication);
            if (adminAuthResult.IsSuccessStatusCode)
            {
                var isAdmin = true;
                await HandleSuccessfulAuthentication(adminAuthResult, isAdmin, userForAuthentication);
                await _localStorage.SetItemAsync("isAdmin", isAdmin);
                return new AuthResponseDto { IsAuthSuccessful = true };
            }

            var userAuthResult = await TryAuthenticate(userTokenEndpoint, userForAuthentication);
            if (userAuthResult.IsSuccessStatusCode)
            {
                var isAdmin = false;
                await HandleSuccessfulAuthentication(userAuthResult, isAdmin, userForAuthentication);
                await _localStorage.SetItemAsync("isAdmin", isAdmin);
                return new AuthResponseDto { IsAuthSuccessful = true };
            }

            var errorResponse = await userAuthResult.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AuthResponseDto>(errorResponse, _options);
        }


        private async Task<HttpResponseMessage> TryAuthenticate(string endpoint, UserForAuthenticationDto userForAuthentication)
        {
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await _client.PostAsync(endpoint, bodyContent);
        }

        private async Task HandleSuccessfulAuthentication(HttpResponseMessage authResult, bool isAdmin, UserForAuthenticationDto userForAuthentication)
        {
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthResponseDto>(authContent, _options);
            await _localStorage.SetItemAsync("authToken", result.token);

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthentication.username);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.token);
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.SetItemAsync("isAdmin", false); // Aggiunta per reimpostare isAdmin a false
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var content = JsonSerializer.Serialize(userForRegistration);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var registrationResult = await _client.PostAsync("https://gruppo6-webapp.azurewebsites.net/api/Users", bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();

            if (!registrationResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, _options);
                return result;
            }

            return new RegistrationResponseDto { IsSuccessfulRegistration = true };
        }
    }
}
