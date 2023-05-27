using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO.User;
using WebApplication1.Models;
using WebApplication1.Services.AuthService;

namespace WebApplication1.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<User> GetUserDetails()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userIdGuid = Guid.Parse(userId);

            var user = await _authService.GetUserDetails(userIdGuid);
            return user;
        }

        [HttpPost("login")]
        public async Task<AuthResponse> SignIn(UserForLogin userForLogin)
        {
            return await _authService.SignIn(userForLogin);

        }
        [HttpPost("register")]
        public async Task<AuthResponse> SignГз(UserForRegistration userForRegistration)
        {
            return await _authService.SignUp(userForRegistration);
        }
    }
}
