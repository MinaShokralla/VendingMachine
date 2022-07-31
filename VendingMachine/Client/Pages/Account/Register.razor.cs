using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using VendingMachine.Client.Services.AuthService;
using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Pages.Account
{
    public partial class Register
    {
        [Inject] public NavigationManager navigationManager { get; set; }
        [Inject] public IAuthService authService { get; set; }
        [Inject] public ILocalStorageService LocalStorage { get; set; }

        public UserRegisterDto user { get; set; } = new();


        public string message { get; set; } = string.Empty;
        public bool IsBusy { get; set; }
        public bool RegisterSuccessfully { get; set; }



        async Task HandleRegistration()
        {
            IsBusy = true;
            try
            {
                var result = await authService.Register(user);
                if (result != null && result.Success)
                {
                    RegisterSuccessfully = true;
                    message = $"SUCCESS!!.. {result.Message}";
                    IsBusy = false;
                    return;
                }
                message = $"ERROR!!..{result?.Message}";
            }
            catch (Exception)
            {
                message = $"ERROR!!..Server has some problem, please contact the admin for any support";
            }
            IsBusy = false;
        }
    }
}
