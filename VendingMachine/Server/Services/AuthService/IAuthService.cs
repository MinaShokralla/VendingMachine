using VendingMachine.Models;
using VendingMachine.Shared;

namespace VendingMachine.WebAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExists(string username);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
        int GetUserId();
        string GetUsername();
        Task<User?> GetUserByUsername(string username);
    }
}
