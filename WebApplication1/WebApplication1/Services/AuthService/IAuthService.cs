using WebApplication1.DTO.User;
using WebApplication1.Models;

namespace WebApplication1.Services.AuthService
{
    public interface IAuthService
    {
        Task<AuthResponse> SignIn(UserForLogin userForLogin);
        Task<AuthResponse> SignUp(UserForRegistration userForRegistration);
        Task<User> GetUserDetails(Guid id);
        Task<List<User>> GetUsers();
    }
}
