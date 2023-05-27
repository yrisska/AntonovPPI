using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class User : BaseEntity
    {
        [MaxLength(15)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(15)]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public DateOnly Birthday { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        [JsonIgnore]
        public int FailedAuthCount { get; set; } = 0;
        public DateOnly LastAuthDate { get; set; }


        [JsonIgnore]
        public bool IsAdmin { get; set; } = false;

    }
}
