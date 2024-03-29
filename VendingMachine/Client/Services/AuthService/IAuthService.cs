﻿using VendingMachine.Shared;
using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>?> Register(UserRegisterDto request);
        Task<ServiceResponse<string>?> Login(UserLoginDto request);
        Task<ServiceResponse<bool>?> ChangePassword(UserChangePasswordDto request);
        Task<bool?> IsUserAuthenticated(); 
    }
}
