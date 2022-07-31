using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using VendingMachine.Client.Services.AuthService;
using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Pages.Account
{
    public partial class Login
    {
        [Inject] public NavigationManager navigationManager { get; set; }
        [Inject] public IAuthService authService { get; set; }
        [Inject] public ILocalStorageService LocalStorage { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public UserLoginDto user { get; set; } = new();

        public string message { get; set; } = string.Empty;
        public bool IsBusy { get; set; }

        private string returnUrl = string.Empty;


        

        protected override Task OnInitializedAsync()
        {
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnUrl", out var url))
            {
                returnUrl = url;
            }
            return Task.CompletedTask;
        }

        async Task HandleLogin()
        {
            try
            {
                var result = await authService.Login(user);
                if (result != null && result.Success)
                {
                    message = string.Empty;
                    await LocalStorage.SetItemAsync("authToken", result.Data);
                    await AuthenticationStateProvider.GetAuthenticationStateAsync();
                    navigationManager.NavigateTo(returnUrl);
                }
                else
                {
                    message = $"ERROR!!..{result?.Message}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                message = $"ERROR!!..Server has some problem, please contact the admin for any support";
            }

        }
    }
}
