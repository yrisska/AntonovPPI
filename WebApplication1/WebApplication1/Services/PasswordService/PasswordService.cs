using System.Security.Cryptography;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Services.PasswordService
{
    public class PasswordService : IPasswordService
    {
        private const int keySize = 64;
        private const int iterations = 350000;
        private readonly HashAlgorithmName algorithm = HashAlgorithmName.SHA512;
        public void SetUserPassword(User user, string password)
        {
            HashPassword(password, out string hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
        }
        private void HashPassword(string password,out string hash, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            hash = Convert.ToHexString(
                Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(password),
                    salt,
                    iterations,
                    algorithm,
                    keySize
                    )
                );
        }
        public bool VerifyUserPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, algorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
