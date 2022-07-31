using Microsoft.AspNetCore.Components;
using VendingMachine.Client.Services.AuthService;
using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Pages.Account
{
    public partial class Changepassword
    {
        [Inject] public NavigationManager navigationManager { get; set; }
        [Inject] public IAuthService authService { get; set; }

        public UserChangePasswordDto user { get; set; } = new();


        public string message { get; set; } = string.Empty;
        public bool IsBusy { get; set; }
        public bool ChangePasswordSuccessfully { get; set; }


        async Task HandleChangePassword()
        {
            IsBusy = true;
            try
            {
                var result = await authService.ChangePassword(user);
                if (result != null && result.Success)
                {
                    ChangePasswordSuccessfully = true;
                    message = $"SUCCESS!!.. Password Updated Successfully";
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
