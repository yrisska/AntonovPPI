using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO.User
{
    public class UserForLogin
    {
        [EmailAddress]
        [DefaultValue("jane.doe@example.com")] // Testing purpose
        public string Email { get; set; } = string.Empty;
        [DefaultValue("admin")] // Testing purpose
        public string Password { get; set; } = string.Empty;
    }
}
