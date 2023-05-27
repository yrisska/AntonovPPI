using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.DTO.User;
using WebApplication1.Models;
using WebApplication1.Services.PasswordService;

namespace WebApplication1.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _configuration;
        private readonly List<User> _users;
        public AuthService(IPasswordService passwordService, IConfiguration configuration)
        {
            _passwordService = passwordService;
            _configuration = configuration;

            _users = new()
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Alex",
                    LastName = "Noxwill",
                    Birthday = new DateOnly(1979, 7, 26),
                    Email = "alex.noxwill@example.com",
                    LastAuthDate = DateOnly.FromDateTime(DateTime.Now),
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jane",
                    LastName = "Doe",
                    Birthday = new DateOnly(1999, 4, 16),
                    Email = "jane.doe@example.com",
                    LastAuthDate = DateOnly.FromDateTime(DateTime.Now),
                    IsAdmin = true,
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Smith",
                    Birthday = new DateOnly(1964, 4, 14),
                    Email = "john.smith@example.com",
                    LastAuthDate = DateOnly.FromDateTime(DateTime.Now),
                },
                
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Alex",
                    LastName = "Johnson",
                    Birthday = new DateOnly(1985, 9, 8),
                    Email = "alex.johnson@example.com",
                    LastAuthDate = DateOnly.FromDateTime(DateTime.Now),
                },

                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Sarah",
                    LastName = "Thompson",
                    Birthday = new DateOnly(1992, 2, 20),
                    Email = "sarah.thompson@example.com",
                    LastAuthDate = DateOnly.FromDateTime(DateTime.Now),
                },

                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Robert",
                    LastName = "Davis",
                    Birthday = new DateOnly(1978, 11, 30),
                    Email = "robert.davis@example.com",
                    LastAuthDate = DateOnly.FromDateTime(DateTime.Now),
                },

                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Emily",
                    LastName = "Wilson",
                    Birthday = new DateOnly(1989, 6, 10),
                    Email = "emily.wilson@example.com",
                    LastAuthDate = DateOnly.FromDateTime(DateTime.Now),
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Michael",
                    LastName = "Johnson",
                    Birthday = new DateOnly(1983, 5, 12),
                    Email = "michael.johnson@example.com",
                    LastAuthDate = DateOnly.FromDateTime(DateTime.Now),
                },

                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jessica",
                    LastName = "Adams",
                    Birthday = new DateOnly(1991, 8, 28),
                    Email = "jessica.adams@example.com",
                    LastAuthDate = DateOnly.FromDateTime(DateTime.Now),
                },

                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "David",
                    LastName = "Brown",
                    Birthday = new DateOnly(1975, 3, 19),
                    Email = "david.brown@example.com",
                    LastAuthDate = DateOnly.FromDateTime(DateTime.Now),
                },
            };

            _passwordService.SetUserPassword(_users[0], "test");
            _passwordService.SetUserPassword(_users[1], "admin");
            _passwordService.SetUserPassword(_users[2], "111");
            _passwordService.SetUserPassword(_users[3], "111");
            _passwordService.SetUserPassword(_users[4], "111");
            _passwordService.SetUserPassword(_users[5], "111");
            _passwordService.SetUserPassword(_users[6], "111");
            _passwordService.SetUserPassword(_users[7], "111");
            _passwordService.SetUserPassword(_users[8], "111");
            _passwordService.SetUserPassword(_users[9], "111");
        }

        public async Task<User> GetUserDetails(Guid id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException("No user with such id");

            return await Task.FromResult(user);
        }

        public async Task<AuthResponse> SignIn(UserForLogin userForLogin)
        {
            var user = _users.FirstOrDefault(x => x.Email == userForLogin.Email) ?? throw new KeyNotFoundException("No user with such email");

            AuthResponse response;

            if (_passwordService.VerifyUserPassword(userForLogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                user.LastAuthDate = DateOnly.FromDateTime(DateTime.Now);

                response = new AuthResponse { 
                    IsSuccesfull = true, 
                    UserId = user.Id, 
                    Token = CreateToken(user),
                };
            }
            else
            {
                user.FailedAuthCount++;

                response = new AuthResponse { IsSuccesfull = false };
            }
            return await Task.FromResult(response);
        }

        async Task<AuthResponse> IAuthService.SignUp(UserForRegistration userForRegistration)
        {
            var user = userForRegistration.ToModel();

            _passwordService.SetUserPassword(user, userForRegistration.Password);
            user.Id = Guid.NewGuid();

            _users.Add(user);

            var authResponse = new AuthResponse
            {
                IsSuccesfull = true,
                UserId = user.Id,
                Token = CreateToken(user),
            };

            return await Task.FromResult(authResponse);
        }   
        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, value: $"{user.FirstName} {user.LastName}"),
            };

            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["secretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<List<User>> GetUsers()
        {
            return await Task.FromResult(_users);
        }
    }
}
