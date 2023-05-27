using WebApplication1.Models;

namespace WebApplication1.Services.PasswordService
{
    public interface IPasswordService
    {
        void SetUserPassword(User user, string password);
        bool VerifyUserPassword(string password, string hash, byte[] salt);
    }
}
