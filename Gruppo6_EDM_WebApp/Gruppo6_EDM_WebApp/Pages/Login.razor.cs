using Entities.DTO;
using Microsoft.AspNetCore.Components;
using Gruppo6_EDM_WebApp.Services.AuthProviders.AuthenticationService;
using Blazored.LocalStorage;

namespace Gruppo6_EDM_WebApp.Pages
{
    public partial class Login
    {
        private UserForAuthenticationDto _userForAuthentication = new UserForAuthenticationDto();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ILocalStorageService LocalStorage { get; set; } // Aggiunta dell'iniezione del servizio LocalStorage
        public bool ShowAuthError { get; set; }
        public string Error { get; set; }

        public async Task ExecuteLogin()
        {
            ShowAuthError = false;

            var result = await AuthenticationService.Login(_userForAuthentication);
            if (!result.IsAuthSuccessful)
            {
                Error = result.ErrorMessage;
                ShowAuthError = true;
            }
            else
            {
                var isAdmin = await LocalStorage.GetItemAsync<bool>("isAdmin"); // Utilizzo del servizio LocalStorage
                if (isAdmin)
                {
                    NavigationManager.NavigateTo("/administrator");
                }
                else
                {
                    NavigationManager.NavigateTo("/codice");
                }
            }
        }
    }
}
