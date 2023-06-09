using Dziennik.Client.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel;

namespace Dziennik.Client.Service
{
    public class BaseService
    {
        protected readonly HttpClient _httpClient;
        protected readonly AuthenticationStateProvider authStateProvider;
        protected readonly NavigationManager navigationManager;

        public BaseService(HttpClient httpClient, AuthenticationStateProvider authenticationState, NavigationManager navigation)
        {
            _httpClient = httpClient;
            authStateProvider = authenticationState;
            navigationManager = navigation;
        }

        public async Task<string> GetToken()
        {
            var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
            var token = await customAuthStateProvider.GetToken();
            Console.WriteLine(token);
            if (token == null)
            {
                navigationManager.NavigateTo("/login");
            }
            return token;
        }
    }
}
