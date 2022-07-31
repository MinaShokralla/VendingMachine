using Microsoft.AspNetCore.Components.Authorization;
using VendingMachine.Shared;
using VendingMachine.Shared.Dto;
using System.Net.Http.Json;

namespace VendingMachine.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }



        public async Task<ServiceResponse<string>?> Login(UserLoginDto request)
        {
            var result = await _http.PutAsJsonAsync("api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>?> Register(UserRegisterDto request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
        public async Task<ServiceResponse<bool>?> ChangePassword(UserChangePasswordDto request)
        {
            var result = await _http.PutAsJsonAsync("api/auth/change-password", request.Password);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<bool?> IsUserAuthenticated()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            return authState?.User?.Identity?.IsAuthenticated;
        }


    }
}
