using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.Models;
using System.Security.Claims;
using VendingMachine.Shared;
using VendingMachine.Shared.Dto;
using VendingMachine.WebAPI.Services.AuthService;

namespace VendingMachine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger,
                              IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            _logger.LogInformation($"register api called with {request.Username}");
            var response = await _authService.Register(new User
                                            {
                                                Username = request.Username,
                                                Role = request.Role.ToString()
                                            }, request.Password);
            if (!response.Success)
            {
                _logger.LogWarning($"register api called with {request.Username} Failed");
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            _logger.LogInformation($"login api called with {request.Username}");
            var response = await _authService.Login(request.Username, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("change-password"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword(UserChangePasswordDto ChangePasswordDto)
        {     
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation($"Change Password called by userid {userId}");
            var response = await _authService.ChangePassword(int.Parse(userId), ChangePasswordDto.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
